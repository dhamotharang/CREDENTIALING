var system = require('system');
console.log('read data...');
var inputData = system.stdin.readLine();
var input = JSON.parse(inputData);
console.log(input[0].FirstName);
var page = new WebPage(), testindex = 0, testindex1 = 0, loadInProgress = false, ids = []//, provider = { "firstname": "tulasi", "lastname": "SMITH", "middlename": "LYNN", "NPI": "" };
page.onConsoleMessage = function (msg) {
    //console.log(msg);
};
page.onLoadStarted = function () {
    loadInProgress = true;
    //console.log("load started");
};
page.onLoadFinished = function () {
    loadInProgress = false;
    //console.log("load finished");
};
page.viewportSize = { width: 1024, height: 768 };
var steps = [
  function () {
      page.open("https://exclusions.oig.hhs.gov/Default.aspx");
  },
  function () {
      //enter the values and clicking submit
      page.evaluate(function (input) {
          var firstname = input[0].FirstName;
          var lastname = input[0].LastName;
          var fname = document.getElementById('ctl00_cpExclusions_txtSPFirstName');
          fname.value = firstname;
          var lname = document.getElementById('ctl00_cpExclusions_txtSPLastName');
          lname.value = lastname;
          document.getElementById('ctl00_cpExclusions_ibSearchSP').click();
          return;
      }, input);
  },
  function () {
      ids = page.evaluate(function () {
          var pageids = [];
          if (document.getElementById('ctl00_cpExclusions_gvEmployees') !== null) {
              var inputs = document.getElementById('ctl00_cpExclusions_gvEmployees').rows;
              for (var i = 1; i < inputs.length; i++) {
                  var id = document.getElementById('ctl00_cpExclusions_gvEmployees').rows[i].cells[0].getElementsByTagName("a")[0].id;
                  pageids.push(id);
              }
          }
          return pageids;
      });
  },
  function () {

      if (ids.length == 0) {
          page.render("[DestinationFolder]");
          phantom.exit();
      } else {
          var i = 0;
          crawlinginto();
          function crawlinginafter() {
              i++;
              var myVar1 = setInterval(function () {
                  if (!loadInProgress) {
                      crawlinginto();
                      clearInterval(myVar1);
                  }
              }, 50);
          }
          function crawlinginto() {
              //page.render("InForLoop" + ids[i] + ".png");
              if (ids[i] !== undefined) {
                  var id = ids[i];
                  page.evaluate(function (id) {
                      document.getElementById(id).click();
                  }, id);
                  myVar = setInterval(function () {
                      if (!loadInProgress) {
                          gotolink();
                      }
                  }, 50);
                  function gotolink() {
                      //page.render(id + ".png");
                      page.evaluate(function (input) {
                          var firstName = document.getElementsByTagName("tbody")[1].rows[0].cells[1].innerHTML;
                          var MiddleName = document.getElementsByTagName("tbody")[1].rows[1].cells[1].innerHTML;
                          var LastName = document.getElementsByTagName("tbody")[1].rows[2].cells[1].innerHTML;
                          var DOB = document.getElementsByTagName("tbody")[1].rows[3].cells[1].innerHTML;
                          var NPI = document.getElementsByTagName("tbody")[1].rows[4].cells[1].innerHTML;
                          var UPIN = document.getElementsByTagName("tbody")[1].rows[5].cells[1].innerHTML;
                          var ExcType = document.getElementsByTagName("tbody")[1].rows[9].cells[1].innerHTML;
                          var ExcDate = document.getElementsByTagName("tbody")[1].rows[10].cells[1].innerHTML;
                          console.log(firstName + " " + LastName + " " + MiddleName + " " + DOB);
                          if (MiddleName === input[0].MiddleName && DOB === input[0].DOB) {
                              if (NPI !== null || NPI !== "Unknown") {
                                  if (NPI === input[0].NPI) {
                                      page.render("[DestinationFolder]");
                                      phantom.exit();
                                  }
                              } else if (UPIN !== null || UPIN !== "Unknown") {
                                  if (UPIN === input[0].UPIN) {
                                      page.render("[DestinationFolder]");
                                      phantom.exit();
                                  }
                              }
                              page.render("[DestinationFolder]");
                              phantom.exit();
                          } else {
                              document.getElementById('ctl00_cpExclusions_lbBackToSearch').click();
                          }
                      }, input);
                      crawlinginafter();
                      clearInterval(myVar);
                  }
              }
          }
      }
  }
];
interval = setInterval(function () {
    if (!loadInProgress && typeof steps[testindex] == "function") {
        //console.log("step " + (testindex + 1));
        steps[testindex]();
        testindex++;
    }
    if (!loadInProgress && typeof steps[testindex] != "function") {
        setTimeout(function () {
            page.render("[DestinationFolder]");
            //console.log("test complete!");
            phantom.exit();
        }, 10000);
    }
}, 2000);

