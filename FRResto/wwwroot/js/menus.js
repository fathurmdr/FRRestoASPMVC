function scrollToCategory(category) {
    const targetElement = document.getElementById(category);
    if (targetElement) {
        // Menghitung posisi target dari atas halaman
        const targetPosition = targetElement.getBoundingClientRect().top + window.scrollY;

        // Sesuaikan offset (misalnya 80px untuk memberi ruang di bawah header)
        const offset = 80;

        // Scroll ke posisi target dengan offset
        window.scrollTo({
            top: targetPosition - offset,
            behavior: "smooth"
        });
    }
}

document.querySelectorAll('.open-add-to-cart-modal').forEach(button => {
    button.addEventListener('click', function () {
        const title = this.getAttribute('data-title');
        const description = this.getAttribute('data-description');
        const price = this.getAttribute('data-price');
        const options = JSON.parse(this.getAttribute('data-options'));

        document.getElementById('add-to-cart-modal-title').innerText = title;
        document.getElementById('add-to-cart-modal-description').innerText = description;
        document.getElementById('add-to-cart-modal-price').innerText = `Rp ${price}`;

        const optionsContainer = document.getElementById('add-to-cart-form');
        optionsContainer.innerHTML = '';

        options.forEach(option => {
            const optionHTML = `
                <div class="mb-8">
                    <p class="font-semibold text-lg">${option.name}</p>
                    ${option.choices.map(choice => `
                        <div class="py-2 items-center flex gap-2 border-b border-[#C2C2C2]">
                            <label class="flex w-full justify-between font-semibold text-sm">
                                <span>${choice.value}</span>
                                <span>${choice.price > 0 ? `+${choice.price}` : 'Free'}</span>
                            </label>
                            <input type="${option.isMultiple ? 'checkbox' : 'radio'}" 
                                   name="${option.name}" 
                                   value="${choice.value}" 
                                   class="w-5 h-5">
                        </div>
                    `).join('')}
                </div>
            `;
            optionsContainer.insertAdjacentHTML('beforeend', optionHTML);
        });

        openModal();
    });
});

function openModal() {
    document.getElementById('add-to-cart-modal').classList.remove('hidden');
}

function closeModal() {
    document.getElementById('add-to-cart-modal').classList.add('hidden');
}