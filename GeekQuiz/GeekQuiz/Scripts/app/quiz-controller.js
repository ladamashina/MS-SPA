angular.module("QuizApp", [])
    .controller("QuizCtrl",
        function($scope, $http) {
            {
                $scope.answered = false;
                $scope.title = "loading question...";
                $scope.options = [];
                $scope.correctAnswer = false;
                $scope.working = false;

                $scope.answer = function() {
                    return $scope.correctAnswer ? "correct" : "incorrect";
                };
            };
            $scope.nextQuestion = function() {
                $scope.working = true;
                $scope.answered = false;
                $scope.title = "loading question...";
                $scope.options = [];

                $http.get("http://localhost:51359/api/trivia").then(function (res) {
                    var data = res.data;
                    $scope.options = data.options;
                    $scope.title = data.title;
                    console.log(data);
                    $scope.answered = false;
                    $scope.working = false;
                }).catch(function (res) {
                    $scope.title = "Не работает API";
                    $scope.working = false;
                });
            };
            $scope.sendAnswer = function(option) {
                {
                    $scope.working = true;
                    $scope.answered = true;

                    $http.post("http://localhost:51359/api/trivia", { 'questionId': option.questionId, 'optionId': option.id }).then(function (res) {
                        var data = res.data;
                            $scope.correctAnswer = data === true;
                            $scope.working = false;
                        console.log(data);
                        }).catch (function(res) {
                        $scope.title = "Сериалзация сломалась";
                        $scope.working = false;
                    });
                };
            };
        });