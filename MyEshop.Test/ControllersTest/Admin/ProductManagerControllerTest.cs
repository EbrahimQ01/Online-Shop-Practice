﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using MyEshop.Application.Interfaces;
using MyEshop.Application.ViewModels.Category;
using MyEshop.Application.ViewModels.Product;
using MyEshop.Application.ViewModels.Public;
using MyEshop.Application.ViewModels.Tag;
using MyEshop.Domain.Models;
using MyEshop.Mvc.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyEshop.Test.ControllersTest.Admin
{
    public class ProductManagerControllerTest
    {

        private readonly Mock<IProductService> _mockProductService;
        private readonly Mock<ITagService> _mockTagService;
        private readonly Mock<ICategoryService> _mockCategorytService;
        private readonly ProductManagerController _productController;

        public ProductManagerControllerTest()
        {
            _mockProductService = new Mock<IProductService>();
            _mockTagService = new Mock<ITagService>();
            _mockCategorytService = new Mock<ICategoryService>();
            _productController = new ProductManagerController(_mockProductService.Object, _mockTagService.Object, _mockCategorytService.Object);
        }

        [Fact]
        public async Task Test_Index_Get_All_PreviewAdminProduct_Result_3_PreviewAdminProduct()
        {
            _mockProductService.Setup(mps => mps.GetAllPreviewAdminProductsAsync()).Returns(new List<PreviewAdminProductViewModel>
            {
                new PreviewAdminProductViewModel(),
                new PreviewAdminProductViewModel(),
                new PreviewAdminProductViewModel(),
            }.ToAsyncEnumerable());

            var resultProductControllerIndex = _productController.Index() as ViewResult;

            int productsCount = await (resultProductControllerIndex.Model as IAsyncEnumerable<PreviewAdminProductViewModel>).CountAsync();

            Assert.NotNull(resultProductControllerIndex);
            Assert.NotNull(resultProductControllerIndex.Model);
            Assert.Equal(3, productsCount);
        }

        [Fact]
        public void Test_Index_Get_All_PreviewAdminProduct_Result_Not_Found()
        {
            _mockProductService.Setup(mps => mps.GetAllPreviewAdminProductsAsync())
                .Returns((IAsyncEnumerable<PreviewAdminProductViewModel>)null);

            var resultProductControllerIndex = _productController.Index() as ViewResult;

            Assert.NotNull(resultProductControllerIndex);
            Assert.Null(resultProductControllerIndex.Model);
        }

        [Fact]
        public async Task Test_Create_Post_Result_View()
        {
            _mockProductService.Setup(mps => mps.CreateProductAsync(It.IsAny<ProductCreateViewModel>()))
                .ReturnsAsync(new ResultMethodService());

            var productControllerCreate = await _productController.Create(new ProductCreateViewModel()) as RedirectToActionResult;

            Assert.NotNull(productControllerCreate);
            Assert.Equal("Index", productControllerCreate.ActionName);
        }

        [Fact]
        public async Task Test_Create_Result_Model_State_Not_Valid()
        {
            _productController.ModelState.AddModelError("", "");

            var productControllerCreate = await _productController.Create(new ProductCreateViewModel()) as ViewResult;

            Assert.NotNull(productControllerCreate);
            Assert.NotNull(productControllerCreate.Model);
            Assert.False(_productController.ModelState.IsValid);
        }

        [Fact]
        public async Task Test_Create_Result_Error_In_Layer_Service()
        {

            var resultMethodService = new ResultMethodService();
            resultMethodService.AddError("", "");

            _mockProductService.Setup(mps => mps.CreateProductAsync(It.IsAny<ProductCreateViewModel>()))
                .ReturnsAsync(resultMethodService);

            var productControllerCreate = await _productController.Create(new ProductCreateViewModel()) as ViewResult;

            Assert.NotNull(productControllerCreate);
            Assert.NotNull(productControllerCreate.Model);
            Assert.False(_productController.ModelState.IsValid);
        }

        [Fact]
        public async Task Test_Create_Rresult_View_Result()
        {
            var categoriesList = new List<CategoryViewModel>
            {
                new(),
                new(),
                new()
            }.ToAsyncEnumerable();
            var tagsList = new List<TagForSelect>
            {
                new(),
                new(),
                new()
            };

            _mockCategorytService.Setup(mcs => mcs.GetCategoriesChildrenAsync())
                .Returns(categoriesList);

            _mockTagService.Setup(mcs => mcs.GetTagsForSelectAsync())
                .ReturnsAsync(tagsList);

            var resultProductCreate = (await _productController.Create()) as ViewResult;

            var resultProductCreateModel = resultProductCreate.Model as ProductCreateViewModel;

            Assert.NotNull(resultProductCreate);
            Assert.NotNull(resultProductCreateModel);
            Assert.NotNull(resultProductCreateModel.Categories);
            Assert.NotNull(resultProductCreateModel.Tags);
        }

        [Fact]
        public async Task Test_Delete_View_Result()
        {
            _mockProductService.Setup(mockProductService => mockProductService.GetProductDeleteViewByProductIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new ProductDeleteViewModel());


            var resultProductDelete = await _productController.Delete(It.IsAny<int>()) as ViewResult;

            Assert.NotNull(resultProductDelete);
            Assert.NotNull(resultProductDelete.Model);
            Assert.IsType<ProductDeleteViewModel>(resultProductDelete.Model);
        }

        [Fact]
        public async Task Test_Delete_Result_Not_Found()
        {
            _mockProductService.Setup(mockProductService => mockProductService.GetProductDeleteViewByProductIdAsync(It.IsAny<int>()))
               .ReturnsAsync(null as ProductDeleteViewModel);

            var resultProductDelete = await _productController.Delete(It.IsAny<int>()) as NotFoundResult;

            Assert.NotNull(resultProductDelete);
        }
    }
}
