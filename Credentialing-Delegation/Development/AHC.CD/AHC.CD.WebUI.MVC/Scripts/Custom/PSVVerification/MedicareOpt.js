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
page.viewportSize = { width: 1024, height: 768 };
var steps = [
  function () {
      page.open("http://www.ecorpnet.com/PecosSearch.aspx");
  },
  function () {
      page.evaluate(function () {
          var NPINumber = document.getElementById('ctl00_contentMain_txtNPI');
          NPINumber.value = '[ProviderNPINumber]';
          document.getElementById('ctl00_contentMain_btnSearch').click();
          return;
      });
  },
  function () {
      page.render('Before_Submit.png');
      page.evaluate(function () {
          document.getElementById("ctl00_contentMain_grdDetail_ctl00__0").cells[1].getElementsByTagName("a")[0].click()
      });
  },
  function () {
      page.render("aftersubmit.png");
  }
];
interval = setInterval(function () {
    if (!loadInProgress && typeof steps[testindex] == "function") {
        steps[testindex]();
        testindex++;
    }
    if (!loadInProgress && typeof steps[testindex] != "function") {
        setTimeout(function () {
            page.render('[DestinationFolder]');
            //var base64 = page.renderBase64('PNG');
            //console.log(base64);
            phantom.exit();
        }, 3000);
    }
}, 3000);
