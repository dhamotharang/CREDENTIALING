var system = require('system');
var inputData = system.stdin.readLine();
var input = JSON.parse(inputData);
var page = require('webpage').create(),
server = 'https://www.deadiversion.usdoj.gov/webforms/validateLogin.do',
 data = 'deaNum=' + input[0].DEA_Number + '&lname=' + input[0].Last_Name + '&ssn=' + input[0].SSN + '&taxid=' + input[0].TAX_ID + '&buttons.next.x=27&buttons.next.y=11&buttons.next=Login';
page.open(server, 'post', data, function (status) {
    if (status !== 'success') {
    }
    else {
        var dea = input[0].DEA_Number;
        var ua = page.evaluate(function (dea) {
            document.getElementsByName('deaNum')[0].value = dea;
            document.getElementsByName('buttons.next')[0].click();
            return document.getElementsByName('deaNum')[0].value;
        }, dea);
    }
    setTimeout(function () {
        var newlogo = page.evaluate(function () {
            var x = document.getElementsByTagName('table')[3].innerHTML;
            return x;
        });
        page.render('[DestinationFolder]');
        //var base64 = page.renderBase64('PNG');
        //console.log(base64);
        phantom.exit();
    }, 1000);
});