resource "azurerm_resource_group" "order-link-api" {
  name     = "${var.resource_group_name_prefix}-backend"
  location = var.resource_group_location
}