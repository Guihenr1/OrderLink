resource "random_integer" "unique" {
  min = 10000
  max = 99999
}

resource "azurerm_container_group" "rabbitmq" {
  name                = "rabbitmq-container-group"
  location            = azurerm_resource_group.order-link-api.location
  resource_group_name = azurerm_resource_group.order-link-api.name
  os_type             = "Linux"

  container {
    name   = "rabbitmq"
    image  = "rabbitmq:3-management"
    cpu    = "0.5"
    memory = "1.5"

    ports {
      port     = 5672
      protocol = "TCP"
    }
    ports {
      port     = 15672
      protocol = "TCP"
    }
  }

  ip_address_type = "Public"
  dns_name_label  = "rabbitmq-${random_integer.unique.result}"
}