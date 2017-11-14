Medusa.factory("ProjectService", function ($resource) {
    var resource = $resource('/Project/:id', { id: "@id" },
        {
            'getInfo': { method: 'GET' },
            'get': { method: 'GET', isArray: true },
            'update': { method: 'PUT' },
            'save': { method: 'POST' },
            'remove': { method: 'DELETE' }
        });

    return resource;
});