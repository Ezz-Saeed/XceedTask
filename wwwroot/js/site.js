
function deleteProduct(productId) {
    if (!confirm("Are you sure you want to delete this product?")) {
        return;
    }

    fetch(`/Products/Delete?id=${productId}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
    })
        .then(response => {
            if (response.ok) {
                alert('Product deleted successfully.');
                location.reload(); // Reload the page to reflect the changes
            } else {
                alert('Failed to delete the product. Please try again.');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('An error occurred while trying to delete the product.');
        });
}
