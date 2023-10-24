const manufacturers = [];
const machines = [];

const manufacturerForm = document.getElementById("manufacturer-form");
const machineForm = document.getElementById("machine-form");

const manufacturerList = document.getElementById("manufacturer-list");
const machineList = document.getElementById("machine-list");

// POST ("random id" nadajmo se da nece biti previse unosa i da se nece dogoditi preklapanje)
function addManufacturer(name, address) {
    const isNameTaken = manufacturers.some(manufacturer => manufacturer.name === name);

    if (!isNameTaken) {
        const manufacturer = { Id: Math.floor(Math.random() * 1000), name, address };
        manufacturers.push(manufacturer);
        document.getElementById("error-message1").textContent = "";
    } else {
        document.getElementById("error-message1").textContent = "Manufacturer name must be unique";
    }
}

function addMachine(name, price, maxweight, typeofweight) {
    const isNameTaken = machines.some(machine => machine.name === name);

    if (!isNameTaken) {
        const machine = { Id: Math.floor(Math.random() * 1000), name, price, maxweight, typeofweight };
        machines.push(machine);
        document.getElementById("error-message2").textContent = "";
    } else {
        document.getElementById("error-message2").textContent = "Machine name must be unique";
    }
}

// GET
function displayManufacturers() {
    manufacturerList.innerHTML = "";
    for (let i = 0; i < manufacturers.length; i++) {
        const manufacturer = manufacturers[i];
        const li = document.createElement("li");
        li.textContent = "Name: " + manufacturer.name + ", Address: " + manufacturer.address;
        manufacturerList.appendChild(li);
    }
}

function displayMachines() {
    machineList.innerHTML = "";
    for (let i = 0; i < machines.length; i++) {
        const machine = machines[i];
        const li = document.createElement("li");
        li.textContent = "Name: " + machine.name + ", Price: " + machine.price + ", Max Weight: " + machine.maxweight + ", Type of Weight: " + machine.typeofweight;
        machineList.appendChild(li);
    }
}

// DELETE
function deleteManufacturerByName() {
    const name = document.getElementById("manufacturer-name-delete").value;
    const index = manufacturers.findIndex(manufacturer => manufacturer.name === name);
    if (index !== -1) {
        manufacturers.splice(index, 1);
        displayManufacturers();
        document.getElementById("manufacturer-name-delete").value = "";
        document.getElementById("error-message11").textContent = "";
    } else {
        document.getElementById("error-message11").textContent = "Manufacturer name does not exist";
    }
}

function deleteMachineByName() {
    const name = document.getElementById("machine-name-delete").value;
    const index = machines.findIndex(machine => machine.name === name);
    if (index !== -1) {
        machines.splice(index, 1);
        displayMachines();
        document.getElementById("machine-name-delete").value = "";
        document.getElementById("error-message22").textContent = "";
    } else {
        document.getElementById("error-message22").textContent = "Machinename does not exist";
    }
}



manufacturerForm.addEventListener("submit", function(event) {
    event.preventDefault();
    const name = manufacturerForm.elements["manufacturer-name"].value;
    const address = manufacturerForm.elements["manufacturer-address"].value;

    if (name && address) {
        addManufacturer(name, address);
        displayManufacturers();
        manufacturerForm.reset();
    }
});

machineForm.addEventListener("submit", function(event) {
    event.preventDefault();
    const name = machineForm.elements["machine-name"].value;
    const price = machineForm.elements["machine-price"].value;
    const maxweight = machineForm.elements["machine-maxweight"].value;
    const typeofweight = machineForm.elements["machine-typeofweight"].value;

    if (name && price && maxweight && typeofweight) {
        addMachine(name, price, maxweight, typeofweight);
        displayMachines();
        machineForm.reset();
    }
});
