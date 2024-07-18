resource "azurerm_mssql_server" "order-link-api" {
  name                         = "orderlinkapidb"
  resource_group_name          = azurerm_resource_group.order-link-api.name
  location                     = azurerm_resource_group.order-link-api.location
  administrator_login          = "orderlinkadmin"
  administrator_login_password = "0rd3rY0ur0wnF00d"
  version                      = "12.0"
}
