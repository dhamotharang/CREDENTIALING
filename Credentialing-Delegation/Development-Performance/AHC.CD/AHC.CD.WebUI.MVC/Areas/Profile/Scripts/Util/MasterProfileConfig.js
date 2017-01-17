var areaName = "Profile";

require.config({
    baseUrl: rootDir + "/Areas/Profile/Scripts",
    paths: {
        'angularAMD': '/Scripts/Lib/RequireJS/angularAMD'
    },
    shim: {},
    deps: ['Util/MasterProfileApp']
});