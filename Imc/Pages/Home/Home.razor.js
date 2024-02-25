window.isIos = () => {
    const userAgent = window.navigator.userAgent.toLowerCase();
    return /iphone|ipad|ipod/.test( userAgent );
}

window.isInStandaloneMode = () => {
    return ('standalone' in window.navigator) && (window.navigator.standalone);
}