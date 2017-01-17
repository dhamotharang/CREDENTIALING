var page = new WebPage(), testindex = 0, loadInProgress = false;
page.onConsoleMessage = function (msg) {
    console.log(msg);
};
page.onLoadStarted = function () {
    loadInProgress = true;
};
page.onLoadFinished = function () {
    loadInProgress = false;
};
page.viewportSize = { width: 1024, height: 1024 };
page.onNavigationRequested = function (url, type, willNavigate, main) {
}
var steps = [
  function () {
      //Load Login Page
      page.open('http://dhmh.maryland.gov/drugcontrol/cdssearch/SitePages/Home.aspx');
  },
  function () {
      //Enter Credentials
      page.evaluate(function () {
          document.getElementsByTagName('form')[0].elements["ctl00$m$g_3506e84b_9d67_40bf_ab70_e7ab5a6feff9$SPTextSlicerValueTextControl"].value = "[CDSNumber]";
          return;
      });
  },
  function () {
          page.evaluate(function () {
              var arr = document.getElementsByTagName('form');
              var i;
              for (i = 0; i < arr.length; i++) {
                  if (arr[i].getAttribute('action') == '/drugcontrol/cdssearch/SitePages/Home.aspx') {
                      arr[i].submit();
                      return;
                  }
              } 
              return;})
  },
  function () {
      page.evaluate(function () {
          document.getElementsByClassName('ms-vb itx')[0].children[0].click()
      });
  }
];
interval = setInterval(function () {
    if (!loadInProgress && typeof steps[testindex] == 'function') {
        console.log('step ' + (testindex + 1));
        steps[testindex]();
        testindex++;
    }
    if (!loadInProgress && typeof steps[testindex] != 'function') {
        setTimeout(function () {
            page.render('[DestinationFolder]');
            //var base64 = page.renderBase64('PNG');
            //console.log("test complete!");
            phantom.exit();
        }, 10000);
    }
}, 1000);