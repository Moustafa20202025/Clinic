using ClinicModels.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ClinicModels;


namespace DataAcsses.Db
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    { 

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<ClinicEmployee> ClinicEmployees { get; set; }
        public DbSet<ClinicStore> ClinicStores { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorPatientSchedule> DoctorPatientSchedules { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


     
    }
}
