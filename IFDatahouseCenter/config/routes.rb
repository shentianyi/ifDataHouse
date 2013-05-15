IFDatahouse::Application.routes.draw do
  root :to=>"sessions#new"

  controller :sessions do
    get 'login' => :new
    post 'login' => :create
    get 'logout' => :destroy
    get 'index'=>:index
  end

  resources :parts do
    collection do
      post :updata
      get :import
      get :download
    end
  end
  resources :staffs do
    collection do
      post :updata
      get :import
      get :download
      get :search
    end
  end
  resources :entities do
    collection do
      post :updata
      get :import
      get :download
    end
  end

  resources :mappers do
    collection do
      get :cancel
      match :map
      post :domap
      get 'c'=>:create
    end
    member do
      get :select
      get :item
    end
  end

  resources :mapper_items
end
