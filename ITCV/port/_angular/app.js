/// <reference path="../../Views/UserCabinet/CVPartials/_EducationOverview.cshtml" />
var app = angular.module('CV', ['ngAnimate']);


var CV = {
    User: {},
    SkillsDesc: '',
    Education: [],
    Jobs: [],
    OtherCerts: [],
    Purpose: '',
    OtherInfo: '',

};

var personalInfo = {};
var skills = [{ skill: '' }, { skill: '' }, {skill: ''}];
var jobs = [];


app.controller('TabsController', ['$scope', function ($scope) {
    this.tab = 0;
    this.setTab = function (e) {
        //wizardStatus = "modified";
        $scope.$broadcast('TabChanged');
        this.tab = e;

    };
    this.isSet = function (e) {
        return this.tab == e;

    };

}]);

app.controller('PersonalDataController', ['$scope', function ($scope) {
    $scope.info = CV.User;
    $scope.skills = skills;
    $scope.cv = CV;
    $scope.addSkill = function () {
        
        skills.push({ skill: '' });

    };
    $scope.removeSkill = function () {
        skills.pop();
    };

    $scope.CheckData = function (value, valNeeded) {

        if (!value) {
            return 0;
        }
        if (valNeeded) {
            if (!$("#" + valNeeded).valid()) {
                return 1;
            }
        }
        return 2;
    };

    $scope.GetFilledSkills = function () {
        var filledSkills = [];
        for (var i = 0; i < skills.length; i++) {
            if (skills[i].skill) {
                filledSkills.push(skills[i].skill);
            }
        }
        return filledSkills;
    }
    
}]);

app.controller('ValidityController', function () {
    

    

});

app.controller('JobsController', function () {
    this.jobs = CV.Jobs;
    this.jobUnderEdit = {};
    this.jobEditMode = { mode: "Add", index: "" };
    this.AddJob = function () {

        if (!$("#jobsForm").valid()) {
            return;
        }

        if (this.jobEditMode.mode == "Add") {
            
            CV.Jobs.push(this.jobUnderEdit);
        }
        else {
            
            CV.Jobs[this.jobEditMode.index] = this.jobUnderEdit;
            this.jobEditMode = { mode: "Add", index: "" };
        }
        
        this.jobUnderEdit = {};
    }
    this.EditJob = function (e) {
        this.jobEditMode.mode = "Edit";
        this.jobEditMode.index = e;
        this.jobUnderEdit = angular.copy(CV.Jobs[e]);
        
    };

    this.CancelEdit = function () {
        this.jobEditMode = { mode: "Add", index: "" };
        this.jobUnderEdit = {};
    };

    this.RemoveJob = function (e) {
        CV.Jobs.shift(e);
    }

    


});

app.controller('SidebarController', function () {
    this.divShown = '';
    this.ShowDiv = function (e) {
        if (e === this.divShown) {
            this.divShown = '';
        }
        else {
            this.divShown = e;
        }
    };
    this.IsSet = function (e) {
        return this.divShown === e;
    };

});

app.controller('EdusController', ['$scope', '$http', function ($scope, $http) {

    var template = {
        EduBegin: '',
        EduEnd: '',
        Specialty: '',
        Qualification: '',
        University: {
            UniversityName: '',
            City: '',
            Country: '',
            IsHighSchool: false,
        },
        Faculty: {
            FacultyName: '',
        },
    };

    
    
    var mode;

    $scope.edusFromModel = CV.Education;

    $scope.Init = function () {
        mode = { uni: 'list', fac: 'list' };
        editor = {activated: false, item: ''};
        $scope.uniByList = {}; 
        $scope.uniManually = {};
        $scope.facByList = { FacultyName: '' };
        $scope.facManually = { FacultyName: '' };
        $scope.facs = [];
        $scope.editor = { activated: false, item: '', validationMessages: [] };
        
        $scope.eduUnderEdit = {}; //angular.copy(template);
        $scope.eduUnderEdit.University = $scope.uniByList;
        $scope.eduUnderEdit.Faculty = $scope.facByList;
        
    };

    $scope.CheckMode = function (segment, modeToCompare) {
        if (segment == 'uni') {
            return mode.uni == modeToCompare;
        }
        else {
            return mode.fac == modeToCompare;
        };
    };
     
    $scope.ChangeModes = function (uniMode, facMode) {
        uniMode != null && ChangeUniMode(uniMode);
        facMode != null && ChangeFacMode(facMode);
    };

    $scope.Finish = function () {

        var isValid = true;
        $scope.editor.validationMessages = [];

        if (!$scope.eduUnderEdit.University.UniversityName || !$scope.eduUnderEdit.Faculty.FacultyName) {
            $scope.editor.validationMessages.push('Данные о ВУЗе не введены. Продолжение невозможно');
            isValid = false;
        }

        if (!$("#EduForm").valid()) {
            $scope.editor.validationMessages.push('Форма заполнения данных об образовании содержит ошибки. Продолжение невозможно.');
            isValid = false;
        }
        if (mode.uni === 'manual') {
            if (!$("#UnivForm").valid()) {
                $scope.editor.validationMessages.push('Форма заполнения университетов содержит ошибки. Продолжение невозможно.');
                isValid = false;
            }

        }
        if (mode.fac == 'manual') {
            if (!$("#FacForm").valid()) {
                $scope.editor.validationMessages.push('Форма заполнения данных о факультете содержит ошибки. Продолжение невозможно.');
                isValid = false;
            }
        }
        
        if (!isValid) {
            return;
        }

        $scope.eduUnderEdit.mode = mode;
        if ($scope.editor.activated) {
            CV.Education[$scope.editor.item] = $scope.eduUnderEdit;
        }
        else {
            CV.Education.push($scope.eduUnderEdit);
        }

        $scope.Init();
        
    };

    $scope.Edit = function (e) {
        
        $scope.editor.activated = true;
        $scope.editor.item = e;
        var editee = angular.copy(CV.Education[e]);
        $scope.eduUnderEdit = editee;
        editee.mode.uni == 'list' ? $scope.uniByList = $scope.eduUnderEdit.University : $scope.uniManually = $scope.eduUnderEdit.University;
        editee.mode.fac == 'list' ? $scope.facByList = $scope.eduUnderEdit.Faculty : $scope.facManually = $scope.eduUnderEdit.Faculty;
        $scope.ChangeModes(editee.mode.uni, editee.mode.fac);
        

    };

    

    function ChangeUniMode(modeString) {
        if (modeString != mode.uni) {
            if (modeString == 'list') {
                $scope.eduUnderEdit.University = $scope.uniByList;
                $scope.uniManually = {};
                
            }
            else {
                
                $scope.eduUnderEdit.University = $scope.uniManually;
                $scope.uniByList = {};
                          
            }
            mode.uni = modeString;
        }
    };

    function ChangeFacMode(modeString) {
        
        //if (mode.fac != modeString) {
            if (modeString == 'list') {
                $scope.facManually.FacultyName = '';
                AttachFaculty($scope.facByList);
            }
            else {
                $scope.facByList.FacultyName = '';
                AttachFaculty($scope.facManually)
            }
            mode.fac = modeString;
        //}
    };

    function AttachFaculty(fac) {
         
            $scope.eduUnderEdit.Faculty = fac;
    };
        
    

        
    $scope.$watch(function () {
        return $scope.uniByList.UniversityName;
    },
    function (newValue, oldValue) {
        if (newValue !== oldValue) {
            if (newValue) {
                
                $http.post("/ru/UserCabinet/ReturnFaculties", { name: $scope.uniByList.UniversityName }).success(function (data) {
                    $scope.facs = data;
                });
                
            };
                        
        };
             
    }, false);

    

}]);

app.controller('CertificatesController', function () {
    this.editor = { activated: false, item: '' };
    this.certsFromModel = CV.OtherCerts;
    this.cert = {};

    this.SubmitCert = function () {
        if ($("#CertForm").valid()) {
            if (this.editor.activated) {
                this.certsFromModel[this.editor.item] = this.cert;
                this.editor = { activated: false, item: '' };
            }

            else {
                this.certsFromModel.push(this.cert);

            }
            this.cert = {};
            

        }
    };

    this.EditCert = function (e) {
        this.editor.activated = true;
        this.editor.item = e;
        this.cert = angular.copy(CV.OtherCerts[e]);

    };

    this.CancelEdit = function () {
        this.editor = { activated: false, item: '' };
        this.cert = {};
    }
        
});

app.controller('OtherInfoController', function ($http, $window) {
    
    this.otherinfo = CV.OtherInfo;

    
});

app.controller('FinishController', ['$scope', '$http', '$window', function ($scope, $http, $window) {
    $scope.messages = [];
    $scope.status = 'modified';


    $scope.$on("TabChanged", function () {
        $scope.status = 'modified';
        $scope.messages = [];
    });
    
    $scope.Validate = function () {

        
        var validationResult = ValidateAll();
        $scope.status = validationResult.status;

        if (!validationResult.status) {
            
            $scope.messages = ["Ошибок нет!"];
        }

        else {
            
            $scope.messages = validationResult.messages;
            
        }

    };

    $scope.SendAll = function () {
        for (var i = 0; i < skills.length; i++) {
            if (skills[i].skill) {
                CV.SkillsDesc += skills[i].skill + ',';

            }
        }
        CV.SkillsDesc = CV.SkillsDesc.slice(0, -1);

        $http.post('/ru/UserCabinet/Finish', { cv: CV }).success(function (data) {

            $window.location = '/ru/UserCabinet/ShowCV';
        });
    };

}]);


function ValidateAll() {
    var messages = [];
    var status;
    var form = $("#personalDataForm").validate();
    form.settings.ignore = "";

    if (!$("#personalDataForm").valid()) {
        messages.push("Неверно введены данные пользователя.");
        status = "fault";
    }

    for (var i = 0; i < skills.length; i++) {
        if (skills[i].skill) {
            break;
        }
        if (i == (skills.length - 1)) {
            messages.push("Не введено ни одного базового навыка");
            status = "fault";
        }
        
    }

    if (!CV.Jobs.length) {
        messages.push("Не введено ни одного места работы");
        status = "fault";
    }

    if (!CV.Education.length) {
        messages.push("Не введено ни одного образования");
        status = "fault";
    }

    
    return {status: status, messages: messages};

}

function PrepareForEdit(cv) {
    ResolveDates(cv);
    for (var i = 0; i < cv.Education.length; i++) {
        //CV.Education[i].EduBegin = ResolveDate(CV.Education[i].EduBegin, false);
        //CV.Education[i].EduEnd = ResolveDate(CV.Education[i].EduEnd, false);

        cv.Education[i].mode = {};
        if (!cv.Education[i].University.IsApproved) {
            cv.Education[i].mode.uni = 'manual';
            cv.Education[i].mode.fac = 'manual';
        }
        else {

            cv.Education[i].mode.uni = 'list';
            if (cv.Education[i].Faculty.IsApproved) {
                cv.Education[i].mode.fac = 'list';
            }
            else {
                cv.Education[i].mode.fac = 'manual';
            }

        }
    }
}

function ResolveDates(cv) {
    for (var i = 0; i < cv.Education.length; i++) {
        cv.Education[i].EduBegin = ResolveDate(cv.Education[i].EduBegin, false);
        cv.Education[i].EduEnd = ResolveDate(cv.Education[i].EduEnd, false);
    }

    for (var i = 0; i < cv.Jobs.length; i++) {
        cv.Jobs[i].DateOfBeginning = ResolveDate(cv.Jobs[i].DateOfBeginning, false);
        cv.Jobs[i].DateOfEnding = ResolveDate(cv.Jobs[i].DateOfEnding, false);
    }

    for (var i = 0; i < cv.OtherCerts.length; i++) {
        cv.OtherCerts[i].IssueDate = ResolveDate(cv.OtherCerts[i].IssueDate, false);
    }
    cv.User.DateOfBirth = ResolveDate(cv.User.DateOfBirth, true);
}



function ResolveDate(date, isLong) {
    var cleansed = parseInt(date.substr(6));
    var date = new Date(cleansed);
    var result = date.getFullYear() + '-' + (date.getMonth() + 1);
    if (isLong) {
        result += '-' + (date.getDay() + 1);
    }
    return result;
}
