function activeTab(selected) {
    let i, tab, tabs;
    tabs = document.getElementsByClassName("nav-tab");
    for (i = 0; i < tabs.length; i++)
    {
        tabs[i].className = tabs[i].className.replace(" activeTab", "");
    }
    selected.currentTarget.className += " activeTab";
}