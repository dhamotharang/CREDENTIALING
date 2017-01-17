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
      //Load Login Page
      page.open('https://appsmqa.doh.state.fl.us/MQASearchServices/HealthCareProviders');
  },
  function () {
      //Enter Credentials
      page.evaluate(function () {
          var LicenseNumber = document.getElementById('SearchDto_LicenseNumber');
          LicenseNumber.value = '[StateLicenseNumber]';
          return;
      });
  },
  function () {
      //Login
      page.evaluate(function () {
          var inputs = document.getElementsByTagName('input');
          for (var i = 0; i < inputs.length; i++) {
              if (inputs[i].getAttribute('type') == 'submit') {
                  inputs[i].click();
              }
          }
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
            //console.log(base64);
            phantom.exit();
        }, 3000);
    }
}, 50);