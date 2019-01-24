var api = {
    get: function (uri, params) {
        var paramsKeys = Object.keys(params);
        var paramLine = "";
        for(var i = 0; i < paramsKeys.length; i++){
            paramLine += paramsKeys[i] + "=" + encodeURIComponent(params[paramsKeys[i]].toString());
            if (i + 1 !== paramsKeys.length)
                paramLine += "&"
        }
        
        uri = uri + (paramLine !== "" ? "?" + paramLine : "");
        
        return fetch(uri, {
            method: "GET"
        }).then(function(response) {
            if (response.ok)
                return response.json();
        })
    },
    
    post: function (uri, data) {       
        return fetch(uri, {
            method: "POST",
            body: JSON.stringify(data)
        })
            .then(function(response) {
                if (response.ok)
                    return response.json();
            })
    }
};