﻿document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('editProfileBtn').addEventListener('click', function () {
        var editProfileForm = document.getElementById('editProfileForm');
        if (editProfileForm.style.display === 'block') {
            editProfileForm.style.display = 'none';
        } else {
            editProfileForm.style.display = 'block';
        }
    });

    const depositButtons = document.querySelectorAll(".deposit-amount");
    const customAmountInput = document.getElementById("custom-amount");

    depositButtons.forEach(button => {
        button.addEventListener("click", () => {
            customAmountInput.value = button.getAttribute("data-amount");
        });
    });

    customAmountInput.addEventListener("input", function () {
        if (this.value !== '') {
            customAmountInput.placeholder = "Enter custom amount";
        }
    });

    document.getElementById('deposit-now-button').addEventListener('click', function () {
        var selectedAmount = customAmountInput.value;
        if (selectedAmount === '') {
            var placeholderText = customAmountInput.placeholder;
            var amountMatch = placeholderText.match(/\d+/);
            if (amountMatch !== null) {
                selectedAmount = amountMatch[0];
            }
        }

    });
});
