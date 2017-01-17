//var system = require('system');
//var inputData = system.stdin.readLine();
//input = JSON.parse(inputData);
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
      page.open('http://www.abim.org/verify-physician.aspx');
  },
  function () {
      page.evaluate(function () {
          var SearcType = document.getElementsByName('type')[2]
          SearcType.checked = true;
          var NpiNO = document.getElementById('npi');
          NpiNO.value = '[ProviderNPINumber]';
          NpiNO.disabled = false;
          return;
      });
  },
  function () {
      //Search
      page.evaluate(function () {
          document.getElementsByClassName('capsule-button')[0].click()
          return;
      })
  }
];
interval = setInterval(function () {
    if (!loadInProgress && typeof steps[testindex] == 'function') {
        steps[testindex]();
        testindex++;
    }
    if (!loadInProgress && typeof steps[testindex] != 'function') {
        setTimeout(function () {
            page.render('[DestinationFolder]');
            //var base64 = page.renderBase64('PNG');
            phantom.exit();
        }, 3000);
    }
}, 3000);