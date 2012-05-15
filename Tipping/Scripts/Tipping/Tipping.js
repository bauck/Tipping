var mvcActions = [];

function bindUrl(name, url) {
    mvcActions[name] = url;
}

function getUrl(name) {
    return mvcActions[name];
}
