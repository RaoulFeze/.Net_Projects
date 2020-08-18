function setActiveTab(tab) {
    let tabs = document.getElementsByClassName("nav-tab");
        for (let i = 0; i < tabs.length; {
            tabs[i].className = tabs[i].className.replace("acitveTab", "");
        }
        tab.currentTarget.className += " activeTab";
    console.log(tabs.length);
    }