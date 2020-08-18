//function setActiveTab(tab)
//{
//    var tabs = document.getElementsByClassName("nav-tab");
//    for (let i = 0; i < tabs.length; i++)
//    {
//        tabs[i].className = tabs[i].className.replace(/\bacitveTab\b/g, "");
//    }
//    tab.currentTarget.className += " activeTab";
// }

function setActiveTab(currentTab) {
    var activeTab = document.querySelector(".activeTab");
    if (activeTab) {
        activeTab.className = activeTab.className.replace(/\bacitveTab\b/g, "");
    }
    currentTab.className += " activeTab";
}