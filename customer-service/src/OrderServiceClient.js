
class OrderServiceClient {
    constructor(baseUrl) {
      this.baseUrl = baseUrl;
    }
  
    async placeOrder(orderData) {
      return $.ajax({
        url: `${this.baseUrl}/api/orders`,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(orderData)
      });
    }
  
    async trackOrder(orderId) {
      return $.ajax({
        url: `${this.baseUrl}/api/orders/${orderId}`,
        type: 'GET'
      });
    }
  }
  