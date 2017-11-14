// navigations for memu bar on index page

Medusa.directive('navBar', function () {
    return {
        restrict: 'E',
        controller: function ($scope, $location) {
            $scope.menus = [
                {
                    name: "Home",
                    isActive: true,
                    link: "#home"
                }, {
                    name: "Server",
                    isActive: false,
                    link: "#server"
                },
                {
                    name: "Member",
                    isActive: false,
                    link: "#member"
                },
                {
                    name: "Project",
                    isActive: false,
                    link: "#project"
                },
                {
                    name: "Skill",
                    isActive: false,
                    link: "#skill"
                },
                {
                    name: "MemberSkill",
                    isActive: false,
                    link: "#memberskill"
                }
            ];

            // set all of link to not active
            $scope.setActiveMenu = function (item) {
                angular.forEach($scope.menus,
                    function (value, key) {
                        value.active = false;
                    });
                item.active = true;
            };
            var currentLink = $location.path();
            currentLink = currentLink.replace('/','#');
            angular.forEach($scope.menus,
            function (value, key) {
                
                if (currentLink === value.link) {
                    value.active = true; 
                }
            });

        },
        templateUrl: 'partials/nav.html'
    };

     
});
 
function onLoad($scope, $location) {
    var currentLink = $location.path();
    angular.forEach($scope.functions,
                    function (value, key) {
                        if (value.link.substring(1, value.link.length) === currentLink.substring(1, value.link.length)) {
                            value.active = true;
                        }
                    });
}