(function (angular, $) {
    "use strict";

    var app = angular.module("colorVoter", ["ngMessages"]);

    app.controller("VotesController", ["$scope", function($scope) {

        var ctrl = this,
            // Reference to the server hub 
            // (note the camel-cased name $.connection.colorHub instead of ColorHub)
            colorHub = $.connection.colorHub;

        // Establish controller variables
        ctrl.votes = [];
        ctrl.color = "#0033CC";
        ctrl.registerVote = function () { };
        ctrl.upvote = function () { };
        ctrl.hubConnected = false;

        // Declare updateVotes as a function that the server can call on each listening client
        colorHub.client.updateVotes = function (hubVotes) {
            ctrl.votes = hubVotes;
            // Must tell AngularJS that something may have changed since 
            // SignalR called this method without Angular knowing
            $scope.$apply();
        };

        // Start the connection to the hub
        $.connection.hub.start().done(function () {
            // Code to run once the connecton has been established
            ctrl.hubConnected = true;

            ctrl.registerVote = function () {
                // Call the RegisterVote method on the server's ColorHub
                colorHub.server.registerVote(ctrl.color);
            };

            ctrl.upvote = function(hexValue) {
                colorHub.server.registerVote(hexValue);
            };
        });
    }]);

}(window.angular, window.jQuery));