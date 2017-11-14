Medusa.controller("ProjectController", function ($scope, ServerService, ProjectService, $timeout, MemberServices, SkillService) {
    waitingDialog.show('Loading...');
    $scope.projects = ProjectService.get(
        {},
        function (success) {
            ProjectPaging($scope);
            waitingDialog.hide();
        },
        function (error) {

        });

    $scope.availableServers = ServerService.get();
    $scope.availableMembers = MemberServices.GetAllMembers();
    $scope.availableSkills = SkillService.get();
    $scope.FakeAvailableServers = $scope.availableServers;
    $scope.FakeAvailableMembers = $scope.availableMembers;
    $scope.FakeAvailableSkills = $scope.availableSkills;

    ProjectResetForm($scope);

    $scope.browseCategorys = [
        {
            Name: "ID", Flag: false
        },
        {
            Name: "Name", Flag: true
        },
        {
            Name: "Sart Date", Flag: true
        },
        {
            Name: "End Date", Flag: true
        },
        {
            Name: "Servers", Flag: true
        },
        {
            Name: "Members", Flag: true
        },
        {
            Name: "Skills", Flag: true
        }
    ];

    $scope.isAddOrEdit = false;
    $scope.filterType = $scope.browseCategorys[1];

    //$scope.systemOSOptions = ["MAC", "WIN 7", "WIN 8", "WIN XP", "LINUX", "WIN Server 2K", "WIN Server 2K8"];
    //$scope.typeOptions = ["DEV", "BUILD", "Database"];

    $scope.reloadClick = function () {
        waitingDialog.show('Loading...');
        $scope.projects = ProjectService.get(
        {},
        function (success) {
            ProjectPaging($scope);
            waitingDialog.hide();
        },
        function (error) {

        });
    };

    $scope.viewAllMembers = function (project) {
        var allServersMessage = "";
        for (var i = 0; i < project.Servers.length; i++) {
            allServersMessage += (i + 1) + ". " + project.Servers[i].Ip + "\n";
        }
        BootstrapDialog.alert({
            title: 'View All ' + project.Name + ' Servers',
            message: allServersMessage
        });
    };

    $scope.viewAllServers = function (project) {
        var allMembersMessage = "";
        for (var i = 0; i < project.Members.length; i++) {
            allMembersMessage += (i + 1) + ". " + project.Members[i].Name + "\n";
        }
        BootstrapDialog.alert({
            title: 'View All ' + project.Name + ' Members',
            message: allMembersMessage
        });
    };

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

});

function ProjectResetForm($scope) {
    $scope.newId = null;
    $scope.newName = null;
    $scope.newStartDate = null;
    $scope.newEndDate = null;
    $scope.oldServers = null;
    $scope.oldMembers = null;
    $scope.oldSkills = null;
}

function ProjectPaging($scope) {
    $scope.curPage = [];
    $scope.pages = [];
    $scope.deleteList = [];

    for (var j = 0; j < $scope.projects.length / 10; j++) {
        var page = [];
        if ((j + 1) * 10 < $scope.projects.length) {
            for (var k = 0; k < 10; k++) {
                page.push($scope.projects[j * 10 + k]);
            }
        } else {
            for (var k = 0; k < $scope.projects.length - j * 10; k++) {
                page.push($scope.projects[j * 10 + k]);
            }
        }
        $scope.pages.push(page);
        $scope.pages[j].PageNum = j + 1;
        $scope.pages[j].IsActive = false;
    }
    $scope.pages[0].IsActive = true;
    $scope.curPage = $scope.pages[0];
}


