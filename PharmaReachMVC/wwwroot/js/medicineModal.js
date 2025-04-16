document.querySelectorAll('.open-medicine-modal').forEach(button => {
    button.addEventListener('click', function () {
        const medicineId = this.getAttribute('data-id');
        document.getElementById('modalMedicineName').innerText = this.getAttribute('data-name');
        document.getElementById('modalMedicineDescription').innerText = this.getAttribute('data-description');
        document.getElementById('modalMedicinePrice').innerText = this.getAttribute('data-price');
        document.getElementById('modalMedicineImage').src = this.getAttribute('data-imageurl');
        var createdAtDate = new Date(this.getAttribute('data-createdat'));
        document.getElementById('modalMedicineCreatedAt').innerText = createdAtDate.toISOString().split('T')[0];

        document.getElementById('btnGetFree').href = `/Medicines/GetFree/${medicineId}`;
        document.getElementById('btnGive').href = `/Medicines/Give/${medicineId}`;
        document.getElementById('btnBuy').href = `/Medicines/Buy/${medicineId}`;

        const isFree = this.getAttribute('data-isfree').toLowerCase() === 'true';
        const canBeFree = this.getAttribute('data-canbefree').toLowerCase() === 'true';

        document.getElementById('btnGetFree').classList.remove('d-block', 'd-none');
        document.getElementById('btnGive').classList.remove('d-block', 'd-none');
        document.getElementById('btnGetFree').classList.add(isFree ? 'd-block' : 'd-none');
        document.getElementById('btnGive').classList.add(canBeFree ? 'd-block' : 'd-none');
    });
});

const usdToEgpRate = 50;
const egpToUsdRate = 1 / usdToEgpRate;

document.getElementById('currencySwitcher').addEventListener('click', function () {
    var currencySymbol = document.getElementById('currencySymbol');
    var priceElement = document.getElementById('modalMedicinePrice');
    var currentPrice = parseFloat(priceElement.innerText);

    if (currencySymbol.innerText === '$') {
        priceElement.innerText = (currentPrice * usdToEgpRate).toFixed(2);
        currencySymbol.innerText = '£';
        this.innerText = 'Switch to USD';
    } else {
        priceElement.innerText = (currentPrice * egpToUsdRate).toFixed(2);
        currencySymbol.innerText = '$';
        this.innerText = 'Switch to EGP';
    }
});
