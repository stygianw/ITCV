using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace ITCV.Models.Repositories
{
    public enum Transient
    {
        ToEntity,
        ToView,
    }


    

    public class TransientEntity
    {
        private static object constructed;

        private Type GetEntityType(Type viewtype)
        {
            return Type.GetType("ITCV.Models.Entities." + viewtype.ToString().Split('.').Last() + "Entity");
        }

        private Type GetViewType(Type entitytype)
        {
           // string fs = entitytype.ToString().Substring(0, entitytype.ToString().IndexOf("Entity"));

            
            return Type.GetType("ITCV.Models.Views." + entitytype.Name.Substring(0, entitytype.Name.IndexOf("Entity")));

        }

        public object Transit (object orig, object caller, Transient direction)
        {
                  
            Type transient;
            
            if (direction == Transient.ToView)
	        {
                transient = GetViewType(orig.GetType());
	        }
            else 
            {
                transient = GetEntityType(orig.GetType());
            }
            
            object transientObject = Activator.CreateInstance(transient);
            
            foreach (var originalprop in orig.GetType().GetProperties())
	        {
		        foreach (var transientprop in transient.GetProperties())
	            {
		            if (originalprop.Name == transientprop.Name)
	                {
                        if (originalprop.PropertyType.IsValueType || originalprop.PropertyType == typeof(string))
                        {
                            transientprop.SetValue(transientObject, originalprop.GetValue(orig));    
                        }
                        else if(originalprop.GetValue(orig) != null)
                        {
                            object obj = originalprop.GetValue(orig);
                            if (obj is IEnumerable<object>)
                            {
                                Type genericType = obj.GetType().GetGenericArguments().First();
                                Type[] typeArgs = new Type[1];

                                if (direction == Transient.ToView)
                                {
                                    typeArgs[0] = GetViewType(genericType);
                                }
                                else
                                {
                                    typeArgs[0] = GetEntityType(genericType);
                                }

                                Type enumtype = typeof(HashSet<>).MakeGenericType(typeArgs);
                                object constructedCollection = Activator.CreateInstance(enumtype);
                                
                                foreach (var item in (IEnumerable<object>)obj)
                                {
                                    MethodInfo add = constructedCollection.GetType().GetMethod("Add");
                                    add.Invoke(constructedCollection, new object[]{ Transit(item, transientObject, direction) });
                                    //((ICollection<object>)constructedCollection).Add(Transit(item, direction));
                                }

                                transientprop.SetValue(transientObject, constructedCollection);

                                
                            }

                            else
                            {
                                if (caller != null && transientprop.PropertyType == caller.GetType())
                                {
                                    transientprop.SetValue(transientObject, caller);
                                }
                                else
                                {
                                    transientprop.SetValue(transientObject, Transit(originalprop.GetValue(orig), transientObject, direction));
                                }
                            }
                            
                        }
		                
                        break;
	                }        
	            }
	        }

            return transientObject;

            
	
		 
	
        }
    }
}