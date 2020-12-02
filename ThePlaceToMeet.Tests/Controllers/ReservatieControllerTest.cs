using System.Collections.Generic;
using ThePlaceToMeet.Controllers;
using ThePlaceToMeet.Models.Domain;
using ThePlaceToMeet.Tests.Data;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ThePlaceToMeet.Tests.Controllers
{
    public class ReservatieControllerTest
    {
        private readonly ReservatieController _controller;
        private readonly DummyDbContext _context;
        private readonly Mock<IVergaderruimteRepository> _vergaderruimteRepository;
        private readonly Mock<ICateringRepository> _cateringRepository;
        private readonly Mock<IKortingRepository> _kortingRepository;
        private readonly ReservatieViewModel model;

        public ReservatieControllerTest()
        {
            _context = new DummyDbContext();
            _vergaderruimteRepository = new Mock<IVergaderruimteRepository>();
            _kortingRepository = new Mock<IKortingRepository>();
            _cateringRepository = new Mock<ICateringRepository>();
            _controller = new ReservatieController(_vergaderruimteRepository.Object, _cateringRepository.Object, _kortingRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
            model = new ReservatieViewModel(_context.Vergaderruimte)
            {
                AantalPersonen = 10,
                BeginUur = 10,
                Dag = _context.Dag.AddDays(14),
                CateringId = 3,
                StandaardCatering = true,
                Duur = 2
            };
        }

        #region Index
        [Fact]
        public void Index_AantalPersonenIsNull_GeeftModelMetAlleVergaderruimtesDoorAanDefaultView()
        {
            _vergaderruimteRepository.Setup(v => v.GetAll()).Returns(_context.Vergaderruimtes);
            var actionResult = Assert.IsType<ViewResult>(_controller.Index(null));
            var ruimtes = Assert.IsAssignableFrom<IEnumerable<Vergaderruimte>>(actionResult.Model);
            Assert.Equal(9, ruimtes.Count());
            Assert.Null(actionResult.ViewName);
        }

        [Fact]
        public void Index_AantalPersonenIs20_GeeftModelMetVergaderruimtesDieVoldoendeGrootZijnDoorAanDefaultView()
        {
            _vergaderruimteRepository.Setup(v => v.GetByMaxAantalPersonen(20)).Returns(_context.Vergaderruimtes20);
            var actionResult = Assert.IsType<ViewResult>(_controller.Index(20));
            var ruimtes = Assert.IsAssignableFrom<IEnumerable<Vergaderruimte>>(actionResult.Model);
            Assert.Equal(6, ruimtes.Count());
            Assert.Null(actionResult.ViewName);
        }

        [Fact]
        public void Index_GeeftAantalPersonenDoorViaViewData()
        {
            _vergaderruimteRepository.Setup(v => v.GetByMaxAantalPersonen(20)).Returns(_context.Vergaderruimtes20);
            var actionResult = Assert.IsType<ViewResult>(_controller.Index(20));
            Assert.Equal(20, actionResult.ViewData["aantalPersonen"]);
        }

        [Fact]
        public void Index_AantalPersonenTeGrootVoorElkeVergaderruimte_GeeftModelMetLegeLijstDoorAanDefaultView()
        {
            _vergaderruimteRepository.Setup(v => v.GetByMaxAantalPersonen(60)).Returns(new List<Vergaderruimte>());
            var actionResult = Assert.IsType<ViewResult>(_controller.Index(60));
            var ruimtes = Assert.IsAssignableFrom<IEnumerable<Vergaderruimte>>(actionResult.Model);
            Assert.Empty(ruimtes);
            Assert.Null(actionResult.ViewName);
        }
        #endregion

        #region Reserveer HttpGet
        [Fact(Skip = "Not yet implemented")]
        public void ReserveerGet_GeeftReservatieViewModelDoorAanView()
        {
        }
        #endregion

        #region Reserveer HttpPost
        [Fact]
        public void ReserveerPost_GeldigeReservatieGegevens_VoegtReservatieToeAanVergaderruimteEnKlantEnPersisteertDeReservatie()
        {
            _cateringRepository.Setup(b => b.GetBy(3)).Returns(_context.CateringSushi);
            _vergaderruimteRepository.Setup(v => v.GetById(1)).Returns(_context.Vergaderruimte);

            _controller.Reserveer(1, model, _context.Peter);
            Assert.Equal(3, _context.Peter.Reservaties.Count);
            Assert.Equal(7, _context.Vergaderruimte.Reservaties.Count);
            _vergaderruimteRepository.Verify(t => t.SaveChanges(), Times.Once);
        }

         [Fact]
        public void ReserveerPost_GeldigeReservatieGegevens_RedirectsNaarIndex()
        {
            _cateringRepository.Setup(b => b.GetBy(3)).Returns(_context.CateringSushi);
            _vergaderruimteRepository.Setup(v => v.GetById(1)).Returns(_context.Vergaderruimte);
            var result = Assert.IsType<RedirectToActionResult>(_controller.Reserveer(1, model, _context.Peter));
            Assert.Equal("Index", result.ActionName);
        }

        [Fact(Skip = "Not yet implemented")]
        public void ReserveerPost_OngeldigeModelState_RetourneertDefaultView()
        {
        }

        [Fact]
        public void ReserveerPost_DomeinWerptException_RedirectsNaarIndex()
        {
            _cateringRepository.Setup(b => b.GetBy(3)).Returns(_context.CateringSushi);
            _vergaderruimteRepository.Setup(v => v.GetById(1)).Returns(_context.Vergaderruimte);
            model.Dag = _context.Dag; // overlap
            var result = Assert.IsType<RedirectToActionResult>(_controller.Reserveer(1, model, _context.Peter));
            Assert.Equal("Index",result.ActionName);
        }

        #endregion
    }
}