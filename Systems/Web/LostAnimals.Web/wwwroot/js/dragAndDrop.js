window.handleDragAndDrop = {
    init: function (dotNetObject, inputId) {
        const dropZone = document.querySelector('.drop-zone');
        const fileInput = document.getElementById(inputId);

        if (!dropZone || !fileInput) {
            console.error('Drop zone or file input not found');
            return;
        }

        dropZone.addEventListener('dragover', (e) => {
            e.preventDefault();
            e.dataTransfer.dropEffect = 'copy';
            dropZone.classList.add('drag-over');
        });

        dropZone.addEventListener('dragleave', () => {
            dropZone.classList.remove('drag-over');
        });

        dropZone.addEventListener('drop', (e) => {
            e.preventDefault();
            dropZone.classList.remove('drag-over');
            const files = e.dataTransfer.files;
            if (files.length > 0) {
                fileInput.files = files;
                const event = new Event('change', { bubbles: true });
                fileInput.dispatchEvent(event);
            }
        });
    }
};