resource "azurerm_app_service_plan" "order-link-api" {
  name                = "order-link-appservice-plan"
  location            = azurerm_resource_group.order-link-api.location
  resource_group_name = azurerm_resource_group.order-link-api.name

  sku {
    tier = "Standard"
    size = "S1"
  }

  kind     = "Linux"
  reserved = true
}

resource "azurerm_app_service" "kitchen" {
  name                = "kitchen-app-service"
  location            = azurerm_resource_group.order-link-api.location
  resource_group_name = azurerm_resource_group.order-link-api.name
  app_service_plan_id = azurerm_app_service_plan.order-link-api.id

  site_config {
    linux_fx_version = "DOCKER|kitchen"
  }
  app_settings = {
    "WEBSITES_ENABLE_APP_SERVICE_STORAGE" = "false"
  }
}

resource "azurerm_app_service" "order" {
  name                = "order-app-service"
  location            = azurerm_resource_group.order-link-api.location
  resource_group_name = azurerm_resource_group.order-link-api.name
  app_service_plan_id = azurerm_app_service_plan.order-link-api.id

  site_config {
    linux_fx_version = "DOCKER|order"
  }
  app_settings = {
    "WEBSITES_ENABLE_APP_SERVICE_STORAGE" = "false"
  }
}

resource "azurerm_app_service" "api_gateway" {
  name                = "api-gateway-app-service"
  location            = azurerm_resource_group.order-link-api.location
  resource_group_name = azurerm_resource_group.order-link-api.name
  app_service_plan_id = azurerm_app_service_plan.order-link-api.id

  site_config {
    linux_fx_version = "DOCKER|apigate"
  }
  app_settings = {
    "WEBSITES_ENABLE_APP_SERVICE_STORAGE" = "false"
  }
}