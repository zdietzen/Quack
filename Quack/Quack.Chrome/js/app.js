angular.module('app', ['googleSignIn', 'ngResource', 'ui.router'])
                .config(function ($httpProvider, $stateProvider, $urlRouterProvider) {

                    httpProvider.interceptors.push('authInterceptorService');

                    $urlRouterProvider.otherwise('/login');

                    $stateProvider
        .state('login', { url: '/login', templateUrl: '/templates/authentication/login.html', controller: 'LoginController', authenticate: false })

        .state('teacherDashboard', { url: '/teacher', templateUrl: '/templates/teacher/dashboard.html', controller: 'TeacherDashboardController' })
            .state('teacher.approval', { url: '/approval', templateUrl: '/templates/teacher/approval.html', controller: 'ApprovalController', authenticate: false })
            .state('teacher.cohort', { url: '/cohort', templateUrl: '/templates/teacher/cohort.html', controller: 'CohortController', authenticate: false })
            .state('teacher.addCohort', { url: '/addcohort', templateUrl: '/templates/teacher/addcohort.html', controller: 'AddCohortController', authenticate: false })
            .state('teacher.cohortQuacks', { url: '/cohortquacks', templateUrl: '/templates/teacher/cohortquacks.html', controller: 'CohortQuacksController', authenticate: false })
            .state('teacher.editQuack', { url: '/cohort', templateUrl: '/templates/teacher/editquack.html', controller: 'EditQuackController', authenticate: false })
            .state('teacher.grid', { url: '/grid', templateUrl: '/templates/teacher/grid.html', controller: 'TeacherGridController', authenticate: false })
            .state('teacher.addTeacher', { url: '/addTeacher', templateUrl: '/templates/teacher/addteacher.html', controller: 'AddTeacherController', authenticate: false })
            
        .state('studentDashboard', { url: '/student', templateUrl: '/templates/student/dashboard.html', controller: 'StudentDashboardController' })
            .state('student.addQuack', { url: '/addquack', templateUrl: '/templates/student/addquack.html', controller: 'PostsController', authenticate: false })
            

                });
//API link
angular.module('app').value('apiUrl', 'http://localhost:3000/');

// Load authentication data
angular.module('app').run(function ($rootScope, authService, $state) {
    authService.loadAuthData();
   
    $rootScope.$on("$stateChangeStart", function (event, toState, toParams, fromState, fromParams) {
        if (toState.authenticate && !authService.authentication.isAuthenticated
            && !authService.authentication.isAuthorized) {
            $state.go('login');
            event.preventDefault();
        }
    });
    
});
