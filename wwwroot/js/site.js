(() => {
  const layout = document.getElementById("appLayout");
  const toggleBtn = document.getElementById("sidebarToggleBtn");
  const stateKey = "bibliogest_sidebar_collapsed";

  if (!layout || !toggleBtn) {
    return;
  }

  const savedState = window.localStorage.getItem(stateKey);
  if (savedState === "1") {
    layout.classList.add("sidebar-collapsed");
  }

  toggleBtn.addEventListener("click", () => {
    const collapsed = layout.classList.toggle("sidebar-collapsed");
    window.localStorage.setItem(stateKey, collapsed ? "1" : "0");
  });
})();
