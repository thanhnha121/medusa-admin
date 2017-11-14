
Medusa.factory('MemberServices',
    function ($resource) {
        var resource = $resource("/Member/:id",
            { id: "@id" },
            {
                'GetMember': { method: 'GET' },
                'GetAllMembers': { method: 'GET', isArray: true },
                'AddMember': { method: 'POST' },
                'Update': { method: 'PUT' },
                'Delete': { method: 'DELETE' }
            });
        return resource;
    });

