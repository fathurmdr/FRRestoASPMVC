let menu = null;
let cartLength = 0
let inputs = {
    menuId: null,
    basePrice: 0,
    quantity: 1,
    additionals: []
};

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

async function getMenu(id) {
    try {
        const menuRes = await fetch(`/Menus/GetMenu/${id}`);
        const menu = await menuRes.json();
        return menu;
    } catch (error) {
        console.error(error);
        return null;
    }
}

function openModal() {
    const modal = document.getElementById('add-to-cart-modal');
    const container = document.getElementById('add-to-cart-container');

    modal.classList.add('show');
    container.classList.add('show');
}

function closeModal() {
    const modal = document.getElementById('add-to-cart-modal');
    const container = document.getElementById('add-to-cart-container');

    modal.classList.remove('show');
    container.classList.remove('show');

    inputs.quantity = 1;
}

function addQuantity() {
    inputs.quantity = Math.max(1, inputs.quantity + 1)

    const quantityEl = document.getElementById("quantity")
    if (quantityEl) {
        quantityEl.innerHTML = inputs.quantity
        handleInputs()
    }
}

function subtractQuantity() {
    inputs.quantity = Math.max(1, inputs.quantity - 1)

    const quantityEl = document.getElementById("quantity")
    if (quantityEl) {
        quantityEl.innerHTML = inputs.quantity
        handleInputs()
    }
}

function handleInputs() {
    inputs.additionals = []

    menu.additionals.forEach(({ name, isMultiple, options }) => {
        document.querySelectorAll(`input[name="${name}"]:checked`).forEach((el) => {
            const option = options.find((option) => option.value == el.value)
            inputs.additionals.push({
                name: name,
                sku: option.sku,
                price: option?.price ?? 0,
                isMultiple: isMultiple,
                value: el.value,
            })
        })

    })

    // Calculate total
    let totalPrice = (inputs.basePrice + inputs.additionals.reduce((sum, { price }) => sum + price, 0)) * inputs.quantity;

    const totalPriceEl = document.getElementById('totalPrice')

    totalPriceEl.innerText = totalPrice.toLocaleString('id-ID', { style: 'currency', currency: 'IDR' })
}

async function addToCart(e) {
    e.target.disabled = true;

    const cartId = localStorage.getItem("cartId");

    const payload = {
        cartId: cartId,
        menuId: inputs.menuId,
        quantity: inputs.quantity,
        additionals: inputs.additionals.map(({ sku }) => ({ sku }))
    }

    try {
        const addToCartRes = await fetch("Menus/AddToCart", {
            body: payload
        })

        if (addToCartRes.ok) {
            await addToCartRes.json().then((data) => {
                cartLength = data.cartLength
            })
        }
    } catch (error) {
        console.error(error)
    }

    e.target.disabled = false
}


document.querySelectorAll('.open-add-to-cart-modal').forEach(button => {
    button.addEventListener('click', async function () {
        const addToCartContent = document.getElementById('add-to-cart-content');

        const menuId = this.getAttribute('data-menu-id');
        menu = await getMenu(menuId);

        if (!menu) {
            return
        }

        let totalPrice = menu.price;
        inputs.menuId = menu.id;
        inputs.basePrice = menu.price;

        let contentHTML = `
            <div class="mb-6 flex gap-4 px-6">
                <img src="/img/food.webp" class="rounded-xl w-40 h-40" />
                <div class="py-2 w-full h-full flex flex-col justify-between">
                    <div>
                        <h3 class="text-xl font-semibold">${menu.name}</h3>
                        <p class="text-sm">${menu.description}</p>
                    </div>
                    <p class="font-semibold text-end">${menu.price.toLocaleString('id-ID', { style: 'currency', currency: 'IDR' })}</p>
                </div>
            </div>
            <form id="add-to-cart-form" class="flex flex-col h-full">
                {inputs}
                <div class="h-full"></div>
                <div class="sticky left-0 bottom-0 w-full bg-white border-t p-4 drop-shadow-md">
                    <div class="font-semibold flex justify-between items-center mb-3">
                        <p>Quantity</p>
                        <div class="flex gap-2 items-center">
                            <button type="button" onclick="subtractQuantity()" class="bg-primary px-2.5 rounded-full">
                                <i class="bi bi-dash text-base text-white"></i>
                            </button>
                            <span id="quantity">${inputs.quantity}</span>
                            <button type="button" onclick="addQuantity()" class="bg-primary px-2.5 rounded-full">
                                <i class="bi bi-plus text-base text-white"></i>
                            </button>
                        </div>
                    </div>
                    <div class="font-semibold flex justify-between items-center mb-3">
                        <p>Total</p>
                        <p id="totalPrice">${totalPrice.toLocaleString('id-ID', { style: 'currency', currency: 'IDR' })}</p>
                    </div>
                    <button id="add-to-cart" type="button" class="bg-primary py-2 w-full rounded-full text-white">Add to cart</button>
                </div>
            </form>
        `

        contentHTML = contentHTML.replace("{inputs}", menu.additionals.map(additional => {
            const additionalHTML = `
                <div class="mb-8 px-6">
                    <p class="font-semibold text-lg">${additional.name}</p>
                    ${additional.options.map(option => `
                        <div class="py-2 items-center flex gap-2 border-b border-[#C2C2C2]">
                            <label class="flex w-full justify-between font-semibold text-sm">
                                <span>${option.value}</span>
                                <span>${option.price > 0 ? `+${option.price.toLocaleString('id-ID', { style: 'currency', currency: 'IDR' })}` : 'Free'}</span>
                            </label>
                            <input type="${additional.isMultiple ? 'checkbox' : 'radio'}" 
                                    name="${additional.name}" 
                                    value="${option.value}" 
                                    class="w-5 h-5">
                        </div>
                    `).join('')}
                </div>
            `;

            return additionalHTML
        }).join(''));

        addToCartContent.innerHTML = contentHTML;

        document.querySelectorAll('input').forEach(input => {
            input.addEventListener('change', handleInputs)
        });

        document.querySelector('#add-to-cart')?.addEventListener("click", addToCart)

        openModal();
    });
});

