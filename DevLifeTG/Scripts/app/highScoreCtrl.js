(function () {
    'use strict';

var app = angular.module('app', []);
app.controller('highScoreController', ['$scope', '$http', highScoreController]);

function highScoreController($scope, $http) {
    $scope.loading = true;
    $scope.addMode = false;

    $http.get('/api/HighScore/').success(function (data) {
        $scope.highScores = data;
        $scope.loading = false;
    })
        .error(function () {
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
        $http.post('/api/HighScore/', this.newhighscore).success(function (data) {
            alert("Added Successfully!");
            $scope.addMode = false;
            $scope.highScores.push(data);
            $scope.loading = false;
        }).error(function (data) { 
            $scope.error = "An Error has occured while saving score! " + data;
            $scope.loading = false;
        });
    };

    $scope.save = function () {
        alert("Edit");
        $scope.loading = true;
        var score = this.highScore;
        alert(score);
        $http.put('/api/HighScore/' + score.Id, score).success(function (data) {
            alert("Saved Successfully!");
            score.editMode = false;
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while saving score! " + data;
            $scope.loading = false;
        });
    };

    $scope.deletehighscore = function () {
        $scope.loading = true;
        var Id = this.highScore.Id;
        $http.delete('/api/HighScore/' + Id).success(function (data) {
            alert("Deleted Successfully!");
                $.each($scope.highScores, function (i) {
                    if ($scope.highScores[i].Id === Id) {
                        $scope.highScores.splice(i, 1);
                        return false;
                    }
                });
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while saving score! " + data;
            $scope.loading = false;
        });
    };

}

})();