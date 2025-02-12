
  class OrderTrackerUI {
    constructor(client) {
      this.client = client;
      this.bindEvents();
    }
  
    bindEvents() {
      $('#orderForm').submit(e => {
        e.preventDefault();
        this.handlePlaceOrder();
      });
    }
  
    async handlePlaceOrder() {
      const orderData = {
        customerId: $('#customerId').val(),
        deliveryAddress: $('#deliveryAddress').val(),
        latitude: parseFloat($('#latitude').val()),
        longitude: parseFloat($('#longitude').val())
      };
  
      try {
        const orderId = await this.client.placeOrder(orderData);
        this.showAlert(`Order placed! ID: ${orderId}`);
      } catch (error) {
        this.showAlert(`Error: ${error.responseJSON?.error || 'Failed to place order.'}`);
      }
    }
  
    showAlert(message) {
      alert(message);
    }
  }
  
  // Initialize
  $(document).ready(() => {
    const client = new OrderServiceClient('http://localhost:7094');
    new OrderTrackerUI(client);
  });