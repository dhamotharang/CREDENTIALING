var ajax = function (url,singleparam, data, callback, type) {
    var data_array, data_string, idx, req, value;
    if (data == null) {
        data = {};
    }
    if (callback == null) {
        callback = function () { };
    }
    if (type == null) {
        //default to a GET request
        type = 'GET';
    }
    data_array = [];
    if (singleparam == true) {
        for (idx in data) {
            value = data[idx];
            data_array.push("" + idx + "=" + value);
        }
    }
    else  if (singleparam == false){
        for (idx in data.searchTerm) {
            value = data.searchTerm[idx];
            data_array.push("" + idx + "=" + value);
        }
    }
    data_string = data_array.join("&");
   // if (window.XMLHttpRequest) {
        req = new XMLHttpRequest();
    //}
    //else {
    //    req = new ActiveXObject("Microsoft.XMLHTTP");
    //}
    req.open(type, url, false);
    req.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    req.onreadystatechange = function () {
        if (req.readyState === 4 && req.status === 200) {
            return callback(req.responseText);
        }
    };
    req.send(data_string);
    return req;
};



self.addEventListener('message', function (e) {
    ajax(e.data.url, e.data.singleParam, { searchTerm: e.data.searchTerm }, function (data) {
        self.postMessage(data);
    }, 'POST');
}, false);
