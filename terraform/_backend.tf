terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=3.0.0"
    }
  }
  backend "azurerm" {
    resource_group_name  = "infra-ol"
    storage_account_name = "orderlinkinfra"
    container_name       = "tfstate"
    key                  = "tfstateol"
  }
}

provider "azurerm" {
  features {}
}

provider "http" {}

provider "random" {}