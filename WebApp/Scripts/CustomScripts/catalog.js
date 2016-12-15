angular.module('book-catalog', [])
    .controller('books',
    [
        '$scope', '$http',
        function ($scope, $http) {

            $scope.editBookFlag = false;

            $scope.newBook = {};
            $scope.editBook = {};

            getAll();

            function getAll() {
                $http.get('api/book/')
                .then(function (response) {
                    $scope.books = [];
                    response.data.forEach(function (element, index, array) {
                        $scope.books.push({
                            Id: element.Id,
                            Title: element.Title,
                            Author: element.Author,
                            Description: element.Description,
                            Date: element.Date
                        });
                    });
                });
            }

            $scope.add = function () {
                var book = {
                    Title: $scope.newBook.Title,
                    Author: $scope.newBook.Author,
                    Description: $scope.newBook.Description,
                    Date: new Date()
                }

                $http({
                        method: 'post',
                        url: '/api/book',
                        headers: { 'Content-Type': 'application/json' },
                        data: JSON.stringify({
                            Title: book.Title,
                            Author: book.Author,
                            Description: book.Description,
                            Date: book.Date
                        })
                    })
                    .then(function(response) {
                        getAll();
                        $scope.newBook = {};
                    });
            }

            $scope.delete = function (id) {
                $http({
                    method: 'delete',
                    url: '/api/book/' + id
                }).then(function (response) {
                    getAll();
                });
            }

            $scope.edit = function (book) {
                $scope.editBookFlag = true;

                $scope.editBook.Title = book.Title;
                $scope.editBook.Author = book.Author;
                $scope.editBook.Description = book.Description;
                $scope.editBook.Id = book.Id;
                $scope.editBook.Date = book.Date;
            }

            $scope.submitEdit = function(book) {
                $http({
                    method: 'put',
                    url: '/api/book/' + book.Id,
                    headers: { 'Content-Type': 'application/json' },
                    data: JSON.stringify({
                        Id: book.Id,
                        Title: book.Title,
                        Author: book.Author,
                        Description: book.Description,
                        Date: book.Date
                    })
                })
                    .then(function (response) {
                        getAll();
                        $scope.editBook = {};
                        $scope.editBookFlag = false;
                    });
            }
        }
    ]);