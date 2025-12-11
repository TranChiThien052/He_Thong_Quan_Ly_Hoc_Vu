document.addEventListener("DOMContentLoaded", () => {
  const editButtons = document.querySelectorAll(".edit-btn");
  const formSection = document.getElementById("edit-form");
  const listSection = document.getElementById("list-section");

  const idField = document.getElementById("student-id");
  const nameField = document.getElementById("student-name");
  const statusField = document.getElementById("status");
  const majorField = document.getElementById("major");
  const courseField = document.getElementById("course");

  editButtons.forEach(btn => {
    btn.addEventListener("click", (e) => {
      const row = e.target.closest("tr");
      const cells = row.querySelectorAll("td");

      // Lấy dữ liệu từ bảng
      idField.value = cells[1].textContent;
      nameField.value = cells[2].textContent;
      statusField.value = cells[3].textContent;
      majorField.value = cells[4].textContent;
      courseField.value = cells[5].textContent;

      // Ẩn danh sách, hiện form
      listSection.style.display = "none";
      formSection.style.display = "block";
    });
  });

  document.getElementById("cancel-btn").addEventListener("click", () => {
    // Ẩn form, hiện lại danh sách
    formSection.style.display = "none";
    listSection.style.display = "block";
  });
});
