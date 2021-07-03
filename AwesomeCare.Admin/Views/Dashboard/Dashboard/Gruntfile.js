module.exports = function(grunt) {
    grunt.initConfig({
		sass: {
			options: {
                includePaths: ['node_modules/bootstrap-sass/Dashboard/stylesheets']
            },
            dist: {
				options: {
					outputStyle: 'compressed'
				},
                files: [{
                    'Dashboard/css/main.css': 'Dashboard/scss/main.scss', 	                        /* 'All main SCSS' */
                    'Dashboardcss/color_skins.css': 'Dashboard/scss/color_skins.scss', 				/* 'All Color Option' */
                    'Dashboard/css/chatapp.css': 'Dashboard/scss/pages/chatapp.scss', 				/* 'Chat App SCSS to CSS' */
                    'Dashboard/css/inbox.css': 'Dashboard/scss/pages/inbox.scss', 				    /* 'Inbox App SCSS to CSS' */
				}]
            }
        },
        uglify: {
          my_target: {
            files: {
                'Dashboard/bundles/libscripts.bundle.js': ['../Dashboard/vendor/jquery/jquery-3.3.1.min.js','../Dashboard/vendor/bootstrap/js/popper.min.js','../Dashboard/vendor/bootstrap/js/bootstrap.js'], /* main js*/
                'Dashboard/bundles/vendorscripts.bundle.js': ['../Dashboard/vendor/metisMenu/metisMenu.js','../Dashboard/vendor/jquery-slimscroll/jquery.slimscroll.min.js','../Dashboard/vendor/bootstrap-progressbar/js/bootstrap-progressbar.min.js','../assets/vendor/jquery-sparkline/js/jquery.sparkline.min.js'], /* coman js*/
                
                'Dashboard/bundles/mainscripts.bundle.js':['Dashboard/js/common.js'], /*coman js*/

                'Dashboard/bundles/morrisscripts.bundle.js': ['../Dashboard/vendor/raphael/raphael.min.js','../Dashboard/vendor/morrisjs/morris.js'], /* Morris Plugin Js */
                'Dashboard/bundles/knob.bundle.js': ['../Dashboard/vendor/jquery-knob/jquery.knob.min.js'], /* knob js*/
                'Dashboard/bundles/chartist.bundle.js':['../Dashboard/vendor/chartist/js/chartist.min.js','../Dashboard/vendor/chartist-plugin-tooltip/chartist-plugin-tooltip.min.js','../Dashboard/vendor/chartist-plugin-axistitle/chartist-plugin-axistitle.min.js','../Dashboard/vendor/chartist-plugin-legend-latest/chartist-plugin-legend.js','../Dashboard/vendor/chartist/Chart.bundle.js'], /*chartist js*/                
                
                'Dashboard/bundles/fullcalendarscripts.bundle.js': ['../Dashboard/vendor/fullcalendar/moment.min.js'],
                'Dashboard/bundles/jvectormap.bundle.js': ['../Dashboard/vendor/jvectormap/jquery-jvectormap-2.0.3.min.js','../Dashboard/vendor/jvectormap/jquery-jvectormap-world-mill-en.js'],   /* calender page js */
                'Dashboard/bundles/easypiechart.bundle.js': ['../Dashboard/vendor/jquery.easy-pie-chart/dist/jquery.easypiechart.min.js','../Dashboard/vendor/jquery.easy-pie-chart/easy-pie-chart.init.js'],

                'Dashboard/bundles/datatablescripts.bundle.js': ['../Dashboard/vendor/jquery-datatable/jquery.dataTables.min.js','../Dashboard/vendor/jquery-datatable/dataTables.bootstrap4.min.js'], /* Jquery DataTable Plugin Js  */
                'Dashboard/bundles/flotscripts.bundle.js': ['../Dashboard/vendor/flot-charts/jquery.flot.js','../Dashboard/vendor/flot-charts/jquery.flot.resize.js','../Dashboard/vendor/flot-charts/jquery.flot.pie.js','../Dashboard/vendor/flot-charts/jquery.flot.categories.js','../Dashboard/vendor/flot-charts/jquery.flot.time.js'], /* Flot Chart js*/

                }
            }
        }                
    });
    grunt.loadNpmTasks("grunt-sass");
    grunt.loadNpmTasks('grunt-contrib-uglify');
    
    grunt.registerTask("buildcss", ["sass"]);	
    grunt.registerTask("buildjs", ["uglify"]);
};