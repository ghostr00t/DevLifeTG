(function () {
    'use strict';

var app = angular.module('app', []);
app.controller('highScoreController', ['$scope', '$http', highScoreController]);

function highScoreController($scope, $http) {
    $scope.loading = true;
    $scope.addMode = false;
    $scope.highScores = [];

    $http.get("/api/HighScore/")
        .then(
        function (response) {
            $scope.highScores = response.data;
            $scope.loading = false;
        },
        function (error) {
            $scope.error = "An error has occured while loading scores.";
            $scope.loading = false;
        });

    $scope.toggleEdit = function () {
        this.highScore.editMode = !this.highScore.editMode;
    };

    $scope.toggleAdd = function () {
        $scope.addMode = !$scope.addMode;
    };

    $scope.add = function () {    
        $scope.loading = true;
        $http.post("/api/HighScore/", this.newhighscore)
            .then(
            function (response) {
                alert("Added Successfully!");
                $scope.addMode = false;
                $scope.highScores.push(response.data);
                $scope.loading = false;
            },
            function (error, response) {
                $scope.error = "An Error has occured while adding score! " + response.status
                $scope.loading = false;
            });
    };

    $scope.save = function (highScore) {
        alert("Edit");
        $scope.loading = true;
        
        $http({ url: '/api/HighScore/put', data: highScore, method: 'PUT' })
        .then(
            function (response) {
                alert("Saved Successfully!");
                score.editMode = false;
                $scope.loading = false;
            },
            function (error, response) {
                $scope.error = "An Error has occured while saving score! " + response.status;
                $scope.loading = false;
            });
    };

    $scope.deletehighscore = function (highScore) {
        $scope.loading = true;
        
        $http.delete('/api/HighScore/delete?username=' + highScore.UserName)
            .then(
            function (response) {
                alert("Deleted Successfully!");
                $scope.highScores = response.data;
                $scope.loading = false;
            },
            function (error, response) {
                $scope.error = "An Error has occured while deleting score! " + response.status;
                $scope.loading = false;
            });
    };

}

})();