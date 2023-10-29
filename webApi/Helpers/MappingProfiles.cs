using AutoMapper;
using DAL;
using DAL.Models;
using webApi.DTOs;

namespace webApi.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles() 
        {
            CreateMap<BranchDto, Branch>().ReverseMap();
            CreateMap<Room, RoomDto>()
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Location));

            CreateMap<RoomDto, Room>()
                .ForMember(dest => dest.Branch, opt => opt.MapFrom<BranchNameResolverForRoom>())
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom<BranchIdValueResolverForRoom>());


            // CreateMap<Booking,BookindDTO>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<Booking, BookindDTO>()
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Location))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
            //.ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Branch.Id))
           // .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.CustomerId))
            .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.BookingRooms.Select(br => br.Room)));

            CreateMap<BookindDTO, Booking>()
               
                .ForMember(dest => dest.Customer, opt => opt.MapFrom<CustomerNameResolver>())
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom<CustomerIdValueResolver>())
                .ForMember(dest => dest.Branch, opt => opt.MapFrom<BranchNameResolver>())
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom<BranchIdValueResolver>());
        }
    }

    

    //Branch for booking
    public class BranchNameResolver : IValueResolver<BookindDTO, Booking, Branch>
    {
        private readonly AppDbContext _context;

        public BranchNameResolver(AppDbContext context)
        {
            _context = context;
        }

        public Branch Resolve(BookindDTO source, Booking destination, Branch destMember, ResolutionContext context)
        {
            if (source.BranchName != null)
            {
                var BookingBranch = _context.Branches.FirstOrDefault(pc => pc.Location == source.BranchName);
                if (BookingBranch != null)
                {
                    destination.BranchId = BookingBranch.Id;
                    destination.Branch = BookingBranch;
                    return destination.Branch;
                }
            }

            throw new NotImplementedException();
        }
    }

    public class BranchIdValueResolver : IValueResolver<BookindDTO, Booking, int>
    {
        private readonly AppDbContext _context;

        public BranchIdValueResolver(AppDbContext context)
        {
            _context = context;
        }
        public int Resolve(BookindDTO source, Booking destination, int destMember, ResolutionContext context)
        {
            if (source.BookingName != null)
            {
                var BookingBranch = _context.Branches.FirstOrDefault(pc => pc.Location == source.BranchName);
                if (BookingBranch != null)
                {
                    destination.BranchId = BookingBranch.Id;

                    return destination.BranchId;
                }
            }

            return 1;
        }
    }

    //customer

    public class CustomerNameResolver : IValueResolver<BookindDTO, Booking, Customer>
    {
        private readonly AppDbContext _context;

        public CustomerNameResolver(AppDbContext context)
        {
            _context = context;
        }

        public Customer Resolve(BookindDTO source, Booking destination, Customer destMember, ResolutionContext context)
        {
            if (source.CustomerName != null)
            {
                var BookingCustomer = _context.Customer.FirstOrDefault(pc => pc.Name == source.CustomerName);
                if (BookingCustomer != null)
                {
                    destination.CustomerId = BookingCustomer.CustomerId;
                    destination.Customer = BookingCustomer;
                    return destination.Customer;
                }
            }

            throw new NotImplementedException();
        }
    }

    public class CustomerIdValueResolver : IValueResolver<BookindDTO, Booking, int>
    {
        private readonly AppDbContext _context;

        public CustomerIdValueResolver(AppDbContext context)
        {
            _context = context;
        }
        public int Resolve(BookindDTO source, Booking destination, int destMember, ResolutionContext context)
        {
            if (source.BranchName != null)
            {
                var BookingCustomer = _context.Customer.FirstOrDefault(pc => pc.Name == source.CustomerName);
                if (BookingCustomer != null)
                {
                    destination.CustomerId = BookingCustomer.CustomerId;

                    return destination.CustomerId;
                }
            }

            return 1;
        }
    }

    //branch for room
    public class BranchNameResolverForRoom : IValueResolver<RoomDto, Room, Branch>
    {
        private readonly AppDbContext _context;

        public BranchNameResolverForRoom(AppDbContext context)
        {
            _context = context;
        }

        public Branch Resolve(RoomDto source, Room destination, Branch destMember, ResolutionContext context)
        {
            if (source.BranchName != null)
            {
                var RoomBranch = _context.Branches.FirstOrDefault(pc => pc.Location == source.BranchName);
                if (RoomBranch != null)
                {
                    destination.BranchId = RoomBranch.Id;
                    destination.Branch = RoomBranch;
                    return destination.Branch;
                }
            }

            throw new NotImplementedException();
        }
    }

    public class BranchIdValueResolverForRoom : IValueResolver<RoomDto, Room, int>
    {
        private readonly AppDbContext _context;

        public BranchIdValueResolverForRoom(AppDbContext context)
        {
            _context = context;
        }
        public int Resolve(RoomDto source, Room destination, int destMember, ResolutionContext context)
        {
            if (source.BranchName != null)
            {
                var RoomBranch = _context.Branches.FirstOrDefault(pc => pc.Location == source.BranchName);
                if (RoomBranch != null)
                {
                    destination.BranchId = RoomBranch.Id;

                    return destination.BranchId;
                }
            }

            return 1;
        }
    }
}
