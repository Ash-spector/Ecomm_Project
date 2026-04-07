function searchBooks() {

    let text = document.getElementById("searchInput").value.toLowerCase().trim();
    let filter = document.getElementById("searchFilter").value;

    let cards = document.querySelectorAll(".book-card");
    let container = document.getElementById("bookContainer");
    let message = document.getElementById("noResultMessage");

    let matched = [];

    cards.forEach(card => {

        let title = card.getAttribute("data-title");
        let author = card.getAttribute("data-author");

        let match = false;

        if (text === "") {
            match = true;
        }
        else if (filter === "all") {
            match = title.includes(text) || author.includes(text);
        }
        else if (filter === "title") {
            match = title.includes(text);
        }
        else if (filter === "author") {
            match = author.includes(text);
        }

        if (match) {
            card.style.display = "block";
            matched.push(card);
        } else {
            card.style.display = "none";
        }

    });

    // 🔥 Move matched to top
    matched.forEach(card => {
        container.prepend(card);
    });

    // 🔥 Show / Hide message
    if (matched.length === 0 && text !== "") {
        message.style.display = "block";
    } else {
        message.style.display = "none";
    }
}