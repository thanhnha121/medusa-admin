// navigations for memu bar on index page
Medusa.directive('memberNavBar', function () {
    return {
        restrict: 'E',
        controller: function ($scope, $location) {
            $scope.menus = [
            {
                name: "List all member",
                isActive: false,
                link: "#member#allmember"
            },
            {
                name: "Get a member",
                isActive: false,
                link: "#getamember"
            },
            {
                name: "Add new member",
                isActive: false,
                link: "#addmember"
            },
            {
                name: "Update a member",
                isActive: false,
                link: "#updatemember"
            }

            ];

            
        },
       
        templateUrl: 'partials/member/list-all-members.html'
    };
});



Medusa.directive('addMemberNarBar',
    function() {
        return {
            restrict: 'E',
            replace: true,
            template: '<div></div>',
            link: function ($scope, element, attrs) {
                $scope.AddMember = function () {
                    
                }
            }
        }
    });