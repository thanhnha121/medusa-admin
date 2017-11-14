var waitingDialog = waitingDialog || (function ($) {
    'use strict';

    // Creating modal dialog's DOM
    var $dialog = $(
		'<div class="modal fade" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-hidden="true" style="padding-top:25%; overflow-y:visible;">' +
		'<div class="modal-dialog modal-m">' +
		'<div class="modal-content">' +
			'<div class="modal-header"><h3 style="margin:0;"></h3></div>' +
			'<div class="modal-body">' +
				'<div class="progress progress-striped active" style="margin-bottom:0;"><div class="progress-bar" style="width: 100%"></div></div>' +
			'</div>' +
		'</div></div></div>');

    return {
        /**
		 * Opens our dialog
		 * @param message Custom message
		 * @param options Custom options:
		 * 				  options.dialogSize - bootstrap postfix for dialog size, e.g. "sm", "m";
		 * 				  options.progressType - bootstrap postfix for progress bar type, e.g. "success", "warning".
		 */
        show: function (message, options) {
            // Assigning defaults
            if (typeof options === 'undefined') {
                options = {};
            }
            if (typeof message === 'undefined') {
                message = 'Loading';
            }
            var settings = $.extend({
                dialogSize: 'm',
                progressType: '',
                onHide: null // This callback runs after the dialog was hidden
            }, options);

            // Configuring dialog
            $dialog.find('.modal-dialog').attr('class', 'modal-dialog').addClass('modal-' + settings.dialogSize);
            $dialog.find('.progress-bar').attr('class', 'progress-bar');
            if (settings.progressType) {
                $dialog.find('.progress-bar').addClass('progress-bar-' + settings.progressType);
            }
            $dialog.find('h3').text(message);
            // Adding callbacks
            if (typeof settings.onHide === 'function') {
                $dialog.off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
                    settings.onHide.call($dialog);
                });
            }
            // Opening dialog
            $dialog.modal();
        },
        /**
		 * Closes dialog
		 */
        hide: function () {
            $dialog.modal('hide');
        }
    };

})(jQuery);

Medusa.controller("ServerController", function ($scope, ServerService, ProjectService, $timeout) {
    waitingDialog.show('Loading...');
    $scope.servers = ServerService.get(
        {},
        function (success) {
            ServerPaging($scope);
            waitingDialog.hide();
        },
        function (error) {

        }
    );
    $scope.availableProjects = ProjectService.get();
    $scope.FakeAvailableProjects = $scope.availableProjects;
    ServerResetForm($scope);
    $scope.browseCategorys = [
        {
            Name: "ID", Flag: false
        },
        {
            Name: "IP", Flag: true
        },
        {
            Name: "SystemOS", Flag: true
        },
        {
            Name: "Type", Flag: true
        },
        {
            Name: "Status", Flag: true
        },
        {
            Name: "Projects", Flag: true
        },
        {
            Name: "Descriptions", Flag: true
        }
    ];

    $scope.isAddOrEdit = false;
    $scope.filterType = $scope.browseCategorys[1];

    $scope.systemOSOptions = ["MAC", "WIN 7", "WIN 8", "WIN XP", "LINUX", "WIN Server 2K", "WIN Server 2K8"];
    $scope.typeOptions = ["DEV", "BUILD", "Database"];

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
        $("html,body").animate({ scrollTop: $('#right_wrap').position().top }, 500);
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

    $scope.deleteCheckBoxClick = function (server) {
        for (var i = 0; i < $scope.deleteList.length; i++) {
            if ($scope.deleteList[i].Id == server.Id) {
                $scope.deleteList.splice(i, 1);
                return;
            }
        }
        $scope.deleteList.push(server);
    };

    $scope.deleteClick = function () {
        if ($scope.deleteList.length > 0) {
            var deleteMessage = 'Delete ' + $scope.deleteList.length + ' below servers?';
            for (var i = 0; i < $scope.deleteList.length; i++) {
                deleteMessage += "\n" + $scope.deleteList[i].Ip;
            }
            BootstrapDialog.confirm(deleteMessage, function (result) {
                if (result) {
                    for (var i = 0; i < $scope.deleteList.length; i++) {
                        ServerService.remove({ id: $scope.deleteList[i].Id });
                    }
                    for (var i = 0; i < $scope.servers.length; i++) {
                        for (var j = 0; j < $scope.deleteList.length; j++) {
                            if ($scope.servers[i].Id == $scope.deleteList[j].Id) {
                                $scope.servers.splice(i, 1);
                                i--;
                            }
                        }
                    }
                    $scope.deleteList = [];
                    ServerPaging($scope);
                } else {
                }
            }
            );
        }
        else {
            BootstrapDialog.alert({
                title: 'Delete Alert',
                message: 'Choose any server to delete first!'
            });
        }

    };

    $scope.oldFilterInput = "";

    $scope.editClick = function (server) {
        $scope.oldServerBeforeUpdate = {};
        $scope.oldServerBeforeUpdate.Projects = [];
        for (var i = 0; i < server.Projects.length; i++) {
            $scope.oldServerBeforeUpdate.Projects.push(server.Projects[i]);
        }
        $scope.oldServerBeforeUpdate.Id = server.Id;
        $scope.oldServerBeforeUpdate.Ip = server.Ip;
        $scope.oldServerBeforeUpdate.Type = server.Type;
        $scope.oldServerBeforeUpdate.SystemOS = server.SystemOS;
        $scope.oldServerBeforeUpdate.Descriptons = server.Descriptions;
        $scope.oldServerBeforeUpdate.Status = server.Status;

        $scope.FakeAvailableProjects = $scope.availableProjects;
        $scope.newId = server.Id;
        $scope.newIp = server.Ip;
        $scope.newSystemOS = server.SystemOS;
        $scope.newType = server.Type;
        $scope.oldProjects = server.Projects;
        $scope.newDescriptions = server.Descriptions;
        for (var i = 0; i < server.Projects.length; i++) {
            for (var j = 0; j < $scope.FakeAvailableProjects.length; j++) {
                if (server.Projects[i].Id == $scope.FakeAvailableProjects[j].Id) {
                    $scope.FakeAvailableProjects.splice(j, 1);
                }
            }
        }
        if ($scope.isAddOrEdit == false) {
            $scope.isAddOrEdit = true;
            $timeout(function () {
                $("html,body").animate({ scrollTop: $('#addEditTab').position().top }, 400);
            }, 200);
        } else {
            $("html,body").animate({ scrollTop: $('#addEditTab').position().top }, 400);
        }
    };

    $scope.viewProjectsClick = function (server) {
        var allProjectsMessage = "";
        for (var i = 0; i < server.Projects.length; i++) {
            allProjectsMessage += server.Projects[i].Name + "\n";
        }
        BootstrapDialog.alert({
            title: 'View All ' + server.Ip + ' Projects',
            message: allProjectsMessage
        });
    };

    $scope.removeOldProject = function (p) {
        for (var i = 0; i < $scope.oldProjects.length; i++) {
            if ($scope.oldProjects[i].Id == p.Id) {
                $scope.oldProjects.splice(i, 1);
                $scope.FakeAvailableProjects.push(p);
            }
        }
    };

    $scope.assignNewProject = function () {
        for (var i = 0; i < $scope.availableProjects.length; i++) {
            if ($scope.FakeAvailableProjects[i].Id == $scope.assignNewProjectSelected.Id) {
                if ($scope.oldProjects == null) {
                    $scope.oldProjects = [];
                }
                $scope.oldProjects.push($scope.FakeAvailableProjects[i]);
                $scope.FakeAvailableProjects.splice(i, 1);
            }
        }
    };

    $scope.reloadClick = function () {
        waitingDialog.show('Loading...');
        $scope.servers = ServerService.get(
        {},
        function (success) {
            ServerPaging($scope);
            waitingDialog.hide();
        },
        function (error) {

        }
    );
    };

    $scope.filter = function () {
        if ($scope.filterType == null) {
            BootstrapDialog.alert({
                title: 'Filter Alert',
                message: 'Choose category first!'
            });
        } else {
            var filterInputLowerCase = $scope.filterInput + "";
            filterInputLowerCase = filterInputLowerCase.toLowerCase();
            if ($scope.oldFilterInput.length > filterInputLowerCase.length || filterInputLowerCase === "") {
                $scope.oldFilterInput = filterInputLowerCase;
                waitingDialog.show('Loading...');
                $scope.servers = ServerService.get(
                    {},
                    function (success) {
                        ServerPaging($scope);
                        waitingDialog.hide();
                    },
                    function (error) {

                    });
            } else {
                $scope.oldFilterInput = filterInputLowerCase;
                serverFilter($scope, filterInputLowerCase);
            }
        }
    };

    $scope.submitClick = function () {
        var server = null;
        if ($scope.newIp == null || $scope.Ip === "") {
            BootstrapDialog.alert({
                title: 'Submit Alert',
                message: "You've tried to submit a blank server!"
            });
        }
        else {
            if (checkValidIp($scope) == 0) {
                if ($scope.newId === "" || $scope.newId == null) {
                    BootstrapDialog.confirm('Add ' + $scope.newIp + '?', function (result) {
                        if (result) {
                            waitingDialog.show('Loading...');
                            server = new ServerService();
                            server.Id = 1;
                            server.Ip = $scope.newIp;
                            server.SystemOS = $scope.newSystemOS;
                            server.Type = $scope.newType;
                            server.Projects = $scope.oldProjects;
                            server.Descriptons = $scope.newDescriptions;
                            server.$save(
                                {},
                                function (success) {
                                    waitingDialog.hide();
                                },
                                function (error) {
                                    waitingDialog.hide();
                                }
                                );
                            $scope.servers.push(server);
                            ServerPaging($scope);
                            ServerResetForm($scope);
                            $scope.FakeAvailableProjects = ProjectService.get();
                            $("html,body").animate({ scrollTop: $('#right_wrap').position().top }, 500);
                        } else {
                        }
                    }
                    );
                } else {
                    var message = "Update:";
                    if ($scope.oldServerBeforeUpdate.Ip !== $scope.newIp) {
                        message += "\nIP from \"" + $scope.oldServerBeforeUpdate.Ip + "\" to \"" + $scope.newIp + "\"";
                    }
                    if ($scope.oldServerBeforeUpdate.SystemOS !== $scope.newSystemOS) {
                        message += "\nSystemOS from \"" + $scope.oldServerBeforeUpdate.SystemOS + "\" to \"" + $scope.newSystemOS + "\"";
                    }
                    if ($scope.oldServerBeforeUpdate.Type !== $scope.newType) {
                        message += "\nType from \"" + $scope.oldServerBeforeUpdate.Type + "\" to \"" + $scope.newType + "\"";
                    }
                    for (var i = 0; i < $scope.oldServerBeforeUpdate.Projects.length; i++) {
                        var k = 0;
                        for (var j = 0; j < $scope.oldProjects.length; j++) {
                            if ($scope.oldServerBeforeUpdate.Projects[i].Id == $scope.oldProjects[j].Id) {
                                k = 1;
                            }
                        }
                        if (k == 0) {
                            message += "\nRemove \"" + $scope.oldServerBeforeUpdate.Projects[i].Name + "\"";
                        }
                    }

                    for (var i = 0; i < $scope.oldProjects.length; i++) {
                        var k = 0;
                        for (var j = 0; j < $scope.oldServerBeforeUpdate.Projects.length; j++) {
                            if ($scope.oldServerBeforeUpdate.Projects[j].Id == $scope.oldProjects[i].Id) {
                                k = 1;
                            }
                        }
                        if (k == 0) {
                            message += "\nAdd \"" + $scope.oldProjects[i].Name + "\"";
                        }
                    }
                    if ($scope.oldServerBeforeUpdate.Descriptions !== $scope.newDescriptions) {
                        message += "\nDescriptions from \"" + $scope.oldServerBeforeUpdate.Descriptons + "\" to \"" + $scope.newDescriptions + "\"";
                    }
                    if (message !== "Update:") {
                        BootstrapDialog.confirm(message, function (result) {
                            if (result) {
                                waitingDialog.show('Loading...');
                                server = ServerService.getInfo({ id: $scope.newId });
                                server.Id = $scope.newId;
                                server.Ip = $scope.newIp;
                                server.SystemOS = $scope.newSystemOS;
                                server.Type = $scope.newType;
                                server.Projects = $scope.oldProjects;
                                server.Descriptons = $scope.newDescriptions;
                                server.$update(
                                    {},
                                    function (success) {
                                        waitingDialog.hide();
                                    },
                                    function (error) {
                                        waitingDialog.hide();
                                    }
                                    );
                                for (var i = 0; i < $scope.servers.length; i++) {
                                    if ($scope.servers[i].Id == server.Id) {
                                        $scope.servers[i] = server;
                                    }
                                }
                                ServerPaging($scope);
                                ServerResetForm($scope);
                                $scope.FakeAvailableProjects = ProjectService.get();
                                $("html,body").animate({ scrollTop: $('#right_wrap').position().top }, 500);
                            } else {
                            }
                        }
                        );
                    } else {
                        BootstrapDialog.alert({
                            title: 'Submit Alert',
                            message: 'You have no changes on this server!'
                        });
                    }
                }
            }
            else if (checkValidIp($scope) == 1) {
                BootstrapDialog.alert({
                    title: 'Submit Alert',
                    message: 'Something went wrong, check your IP and try again!'
                });
            }
            else if (checkValidIp($scope) == 2) {
                BootstrapDialog.alert({
                    title: 'Submit Alert',
                    message: 'Existed IP!'
                });
            }
        }
    };

    $scope.cancelBtnClick = function () {
        if (($scope.newId !== "" || $scope.newIp !== "")
            && ($scope.newIp !== "" || $scope.newSystemOS !== "" || $scope.newType !== ""
            || $scope.oldProjects !== [] || $scope.newDescriptions != null
            )) {
            BootstrapDialog.confirm('Cancel Editing?', function (result) {
                if (result) {
                    for (var i = 0; i < $scope.servers.length; i++) {
                        if ($scope.servers[i].Id == $scope.newId) {
                            $scope.servers[i].Ip = $scope.oldServerBeforeUpdate.Ip;
                            $scope.servers[i].SystemOS = $scope.oldServerBeforeUpdate.SystemOS;
                            $scope.servers[i].Type = $scope.oldServerBeforeUpdate.Type;
                            $scope.servers[i].Projects = $scope.oldServerBeforeUpdate.Projects;
                            $scope.servers[i].Descriptons = $scope.oldServerBeforeUpdate.Descriptons;
                            $scope.servers[i].Status = $scope.oldServerBeforeUpdate.Status;
                            break;
                        }
                    }
                    ServerResetForm($scope);
                    $scope.FakeAvailableProjects = ProjectService.get();
                    $scope.isAddOrEdit = false;
                    $("html,body").animate({ scrollTop: $('#right_wrap').position().top }, 500);
                } else {
                }
            }
            );
        }
        else {
            $scope.isAddOrEdit = false;
            $("html,body").animate({ scrollTop: $('#right_wrap').position().top }, 500);
        }
    };

    $scope.addNewServerClick = function () {
        if ($scope.isAddOrEdit == false) {
            $scope.isAddOrEdit = true;
            $timeout(function () {
                $("html,body").animate({ scrollTop: $('#addEditTab').position().top }, 500);
            }, 200);
        } else {
            $("html,body").animate({ scrollTop: $('#addEditTab').position().top }, 500);
        }
    };

});

function serverFilter($scope, filterInputLowerCase) {
    var tmp = "";
    if ($scope.filterType.Name === "ID") {
        BootstrapDialog.alert({
            title: 'Filter Alert',
            message: 'Filter category is not supported!'
        });
    }
    if ($scope.filterType.Name === "IP") {
        for (var i = 0; i < $scope.servers.length; i++) {
            tmp = $scope.servers[i].Ip;
            if (tmp.includes(filterInputLowerCase) === false) {
                $scope.servers.splice(i, 1);
                i--;
            }
        }
        ServerPaging($scope);
    }
    else if ($scope.filterType.Name === "SystemOS") {
        for (var i = 0; i < $scope.servers.length; i++) {
            if ($scope.servers[i].SystemOS === null) {
                tmp = "";
            } else {
                tmp = $scope.servers[i].SystemOS;
                tmp = tmp.toLowerCase();
            }
            if (tmp.includes(filterInputLowerCase) === false) {
                $scope.servers.splice(i, 1);
                i--;
            }
        }
        ServerPaging($scope);
    }
    else if ($scope.filterType.Name === "Type") {
        for (var i = 0; i < $scope.servers.length; i++) {
            if ($scope.servers[i].Type === null) {
                tmp = "";
            } else {
                tmp = $scope.servers[i].Type;
                tmp = tmp.toLowerCase();
            }
            if (tmp.includes(filterInputLowerCase) === false) {
                $scope.servers.splice(i, 1);
                i--;
            }
        }
        ServerPaging($scope);
    }
    else if ($scope.filterType.Name === "Status") {
        for (var i = 0; i < $scope.servers.length; i++) {
            if ($scope.servers[i].Status === null) {
                tmp = "";
            } else {
                tmp = $scope.servers[i].Status;
                tmp = tmp.toLowerCase();
            }
            if (tmp.includes(filterInputLowerCase) === false) {
                $scope.servers.splice(i, 1);
                i--;
            }
        }
        ServerPaging($scope);
    }
    else if ($scope.filterType.Name === "Descriptions") {
        for (var i = 0; i < $scope.servers.length; i++) {
            if ($scope.servers[i].Descriptons === null) {
                tmp = "";
            } else {
                tmp = $scope.servers[i].Descriptons;
                tmp = tmp.toLowerCase();
            }
            if (tmp.includes(filterInputLowerCase) === false) {
                $scope.servers.splice(i, 1);
                i--;
            }
        }
        ServerPaging($scope);
    }
    else if ($scope.filterType.Name === "Projects") {
        for (var i = 0; i < $scope.servers.length; i++) {
            var okk = 0;
            for (var j = 0; j < $scope.servers.Projects.length; j++) {
                if ($scope.servers[i].Projects[j].Name === null) {
                    tmp = "";
                } else {
                    tmp = $scope.servers[i].Projects[j].Name;
                    tmp = tmp.toLowerCase();
                }
                if (tmp.includes(filterInputLowerCase) === true) {
                    okk = 1;
                }
            }
            if (okk == 0) {
                $scope.servers.splice(i, 1);
                i--;
            }
        }
        ServerPaging($scope);
    }

}

function checkValidIp($scope) {
    var tmp = $scope.newIp;
    if (tmp.split(".").length != 4) {
        return 1;
    }
    else {
        if (tmp.split(".")[0] > 255 || tmp.split(".")[0] < 1) {
            return 1;
        }
        if (tmp.split(".")[3] > 255 || tmp.split(".")[3] < 1) {
            return 1;
        }
        for (var i = 1; i < 3; i++) {
            if (tmp.split(".")[i] > 255 || tmp.split(".")[i] < 0) {
                return 1;
            }
        }
    }
    for (var i = 0; i < $scope.servers.length; i++) {
        if ($scope.servers[i].Id != $scope.newId && $scope.servers[i].Ip === tmp) {
            return 2;
        }
    }
    return 0;
}

function ServerResetForm($scope) {
    $scope.newId = "";
    $scope.newIp = "";
    $scope.newSystemOS = "";
    $scope.newType = "";
    $scope.oldProjects = [];
    $scope.newDescriptions = "";
}

function ServerPaging($scope) {
    $scope.curPage = [];
    $scope.pages = [];
    $scope.deleteList = [];
    for (var j = 0; j < $scope.servers.length / 10; j++) {
        var page = [];
        if ((j + 1) * 10 < $scope.servers.length) {
            for (var k = 0; k < 10; k++) {
                page.push($scope.servers[j * 10 + k]);
            }
        } else {
            for (var k = 0; k < $scope.servers.length - j * 10; k++) {
                page.push($scope.servers[j * 10 + k]);
            }
        }
        $scope.pages.push(page);
        $scope.pages[j].PageNum = j + 1;
        $scope.pages[j].IsActive = false;
    }
    $scope.pages[0].IsActive = true;
    $scope.curPage = $scope.pages[0];
}