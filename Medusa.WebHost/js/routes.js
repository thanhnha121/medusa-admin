Medusa.config(function($routeProvider) {
    $routeProvider
        .when("/server",
        {
            templateUrl: "partials/server/server.html",
            controller: "ServerController"
        }).when("/home",
        {
            templateUrl: "partials/home/home.html",
            controller: "HomeController"
        })
        .when("/skill",
        {
            templateUrl: "partials/skill/skill.html"
        })
        .when("/project",
        {
            templateUrl: "partials/project/project.html",
            controller: "ProjectController"
        })
        .when("/memberskill",
        {
            templateUrl: "partials/memberskill/memberskill.html"
        })
        .when("/member/allmember",
        {
            templateUrl: "partials/member/member.html",
            controller: 'MemberController'
        }).when("/member",
        {
            templateUrl: "partials/member/member.html",
            controller: 'MemberController'
        })
        .when("/memberAdd",
        {
            templateUrl: "partials/memberskill/memberAdd.html",
            controller: 'AddMemberController'
        }).otherwise("/home");
});