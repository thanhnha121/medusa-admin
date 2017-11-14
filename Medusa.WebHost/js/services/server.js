Medusa.factory("ServerService", function ($resource) {
    var resource = $resource('/Server/:id', { id: "@id" },
        {
            'getInfo': { method: 'GET' },
            'get': { method: 'GET' , isArray: true},
            'update': { method: 'PUT' },
            'save': { method: 'POST' },
            'remove': { method: 'DELETE' }
        });

    return resource;
});