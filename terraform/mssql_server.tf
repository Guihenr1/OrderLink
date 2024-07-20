resource "azurerm_mssql_server" "order-link-api" {
  name                         = "orderlinkapidb"
  resource_group_name          = azurerm_resource_group.order-link-api.name
  location                     = azurerm_resource_group.order-link-api.location
  administrator_login          = "orderlinkadmin"
  administrator_login_password = "0rd3rY0ur0wnF00d"
  version                      = "12.0"
}

resource "azurerm_mssql_firewall_rule" "order-link-api" {
  name             = "AllowMyIP"
  server_id        = azurerm_mssql_server.order-link-api.id
  start_ip_address = chomp(data.http.my_ip.response_body)
  end_ip_address   = chomp(data.http.my_ip.response_body)
}
