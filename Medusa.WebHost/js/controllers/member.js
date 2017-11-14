Medusa.controller('MemberController',
    function ($scope, MemberServices, ProjectService, $timeout) {
        $scope.members = MemberServices.GetAllMembers();
        $scope.Projects = ProjectService.get();
        $scope.modeDisplay = 'listMember';


        // To paging 
        $scope.browseCategorys = [
            {
                Name: "Name",
                Flag: true
            },
            {
                Name: "User name",
                Flag: true
            },
            {
                Name: "Type",
                Flag: true
            },
            {
                Name: "Address",
                Flag: true
            },
            {
                Name: "Birth day",
                Flag: true
            },
            {
                Name: "Email",
                Flag: true
            },
            {
                Name: "Projects",
                Flag: true
            }
        ];

        $scope.curPage = [];
        $scope.pages = [];

        $timeout(function () {
            MemberPaging($scope);
            $scope.pages[0].IsActive = true;
            $scope.curPage = $scope.pages[0];
        }, 1000);

        $scope.pageClick = function (page) {
            for (var i = 0; i < $scope.pages.length; i++) {
                $scope.pages[i].IsActive = false;
            }
            for (var j = 0; j < $scope.pages.length; j++) {
                if ($scope.pages[j].PageNum == page.PageNum) {
                    $scope.curPage = $scope.pages[j];
                    $scope.pages[j].IsActive = true;
                }
            }
        };

        $scope.changeBrowseCategory = function (bc) {
            for (var i = 0; i < $scope.browseCategorys.length; i++) {
                if ($scope.browseCategorys[i].Name === bc.Name) {
                    if ($scope.browseCategorys[i].Flag === false) {
                        $scope.browseCategorys[i].Flag = true;
                    } else {
                        $scope.browseCategorys[i].Flag = false;
                    }
                }
            }

        };

        // To get all member records
        $scope.getAllMember = function () {
            $scope.members = MemberServices.GetAllMembers();
            $scope.Projects = ProjectService.get();
        };
        //To edit an exitsting member
        $scope.Edit = function (member) {
            $scope.memberInfo = member;
            $scope.modeDisplay = "detailMember";
        };

        // To hide and show form add and edit
        $scope.myVar = false;
        $scope.myClick = function () {
            $scope.myVar = !$scope.myVar;
        };

        // To get a member
        $scope.getAMember = function () {

        };

        // To clear all input controls
        $scope.ClearModels = function () {
            $scope.memberInfo.Name = "";
            $scope.memberInfo.Type = "";
            $scope.memberInfo.Address = "";
            $scope.memberInfo.Birthday = "";
            $scope.memberInfo.Email = "";
            $scope.memberInfo.Projects = "";
            $scope.memberInfo.UserName = "";
            $scope.memberInfo.Password = "";
        };

        // To display detail member form
        $scope.showDetailMemberForm = function () {
            //$scope.ClearModels();
            $scope.modeDisplay = "detailMember";


        };
        //To show all member
        $scope.showAllMember = function () {
            $scope.modeDisplay = "listMember";
        };

        $scope.submitMember = function () {
            console.log($scope.memberInfo);
            if ($scope.memberInfo.Id != 0 || $scope.memberInfo.Id != "") {
                MemberServices.Update($scope.memberInfo,
                    function () {
                        $scope.getAllMember();
                        $scope.modeDisplay = "both";
                    },
                    function () { alert("Can not update!"); });

            } else {
                MemberServices.AddMember($scope.memberInfo,
                    function () {
                        $scope.getAllMember();
                        $scope.modeDisplay = "both";
                        $scope.memberInfo = {};

                    },
                    function () {
                        alert("ERROR");
                    });
            }

        };
        // To delete a member
        $scope.delete = function (x) {
            MemberServices.remove({ id: x.Id },
                function () {
                });
            for (var i = 0; i < $scope.members.length; i++) {
                if ($scope.members[i].Id === x.Id) {
                    $scope.members.splice(i, 1);
                }
            }
        };
        $scope.getAllMember();
    });


function MemberPaging($scope) {
    for (var j = 0; j < $scope.members.length / 10; j++) {
        var page = [];
        if ((j + 1) * 10 < $scope.members.length) {
            for (var k = 0; k < 10; k++) {
                page.push($scope.members[j * 10 + k]);
            }
        } else {
            for (var k = 0; k < $scope.members.length - j * 10; k++) {
                page.push($scope.members[j * 10 + k]);
            }
        }
        $scope.pages.push(page);
        $scope.pages[j].PageNum = j + 1;
        $scope.pages[j].IsActive = false;
    }
}
