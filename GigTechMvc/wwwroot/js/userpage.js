const editProfileBtn = document.getElementById('editProfileBtn');
const editProfileForm = document.getElementById('editProfileForm');

// Add event listener to the button
editProfileBtn.addEventListener('click', function () {
    // Toggle the display style of the form
    if (editProfileForm.style.display === 'none') {
        editProfileForm.style.display = 'block';
    } else {
        editProfileForm.style.display = 'none';
    }
});

document.querySelectorAll('.depositAmount').forEach(button => {
    button.addEventListener('click', function () {
        const clickedAmount = parseInt(this.getAttribute('data-amount'));
        document.getElementById('VMoney').value = clickedAmount;
    });
});


let buttons = document.querySelectorAll('.profileImageButton');

buttons.forEach(function (button) {
    button.addEventListener('click', function () {
        button.style.backgroundColor = "red";
    });
});