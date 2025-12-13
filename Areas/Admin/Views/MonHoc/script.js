document.addEventListener("DOMContentLoaded", () => {
    const addBtn = document.querySelector(".btn-add");
    const addSection = document.querySelector("#add-subject");
    const listSection = document.querySelector("#list-subject");

    // Form inputs
    const idSubject = document.getElementById("subject-id");

    addBtn.addEventListener("click", () => {
        // 1. Toggle visibility
        listSection.style.display = "none";
        addSection.style.display = "block";
        addBtn.style.display = "none"; // Hide add button itself if desired, or keep it

        // 2. Generate unique ID
        const newId = generateUniqueId();
        idSubject.value = newId;
    });

    function generateUniqueId() {
        const prefix = "MH";
        const ids = window.existingIds || [];
        
        // Find existing numbers with MH prefix
        const numbers = ids
            .filter(id => id.startsWith(prefix))
            .map(id => parseInt(id.replace(prefix, ""), 10))
            .filter(n => !isNaN(n));

        // Find max
        let nextNum = 1;
        if (numbers.length > 0) {
            nextNum = Math.max(...numbers) + 1;
        }

        // Format as MHxxx
        return prefix + nextNum.toString().padStart(3, '0');
    }
});