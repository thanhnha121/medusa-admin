
Medusa.factory('MemberServices',
    function ($resource) {
        var url = "/Member/:id";
        var resource = $resource(url,
            { id: "@id" },
            {
                'GetMember': { method: 'GET', isArray: true },
                'GetAllMembers': { method: 'GET', isArray: true },
                'AddMember': { method: 'POST' },
                'Update': { method: 'PUT' },
                'Delete': { method: 'DELETE' }
            });
        return resource;
    });

